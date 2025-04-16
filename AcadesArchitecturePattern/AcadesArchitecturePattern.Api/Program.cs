using AcadesArchitecturePattern.Application.Handlers.Authentications;
using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Application.Handlers.Tasks;
using AcadesArchitecturePattern.Application.Handlers.ToDoLists;
using AcadesArchitecturePattern.Application.Handlers.Users;
using AcadesArchitecturePattern.Application.Services;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Security;
using AcadesArchitecturePattern.Infra.Data.SQLite.Contexts;
using AcadesArchitecturePattern.Infra.Data.SQLite.Services;
using AcadesArchitecturePattern.Shared.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using Serilog;  // Certifique-se de adicionar essa referência
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// --- Serilog configuration for file logging ---
var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var logFolder = Path.Combine(baseFolder, "AcadesArchitecturePattern", "Logs");
// Creates the folder if it doesn't exist
Directory.CreateDirectory(logFolder);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Set the minimum level (can be adjusted based on the environment)
    .Enrich.FromLogContext()     // Enrich the logs with context information
    .WriteTo.Console()           // Optional: to display on the console
    .WriteTo.File(
         Path.Combine(logFolder, "log-.txt"),
         rollingInterval: RollingInterval.Day,  // Creates a new file each day
         retainedFileCountLimit: 7)               // For example: retains the last 7 files
    .CreateLogger();

// Integrates Serilog with the Generic Host
builder.Host.UseSerilog();

// --- Default/existing configurations ---
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Adding Swagger authorization
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AcadesArchitecturePattern.Api", Version = "v1" });

    // Setting Swagger security definition for Bearer authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token.\r\nExample: \"Bearer 12345abcdef\"",
    });

    // Requiring Bearer security for Swagger operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// Adding CORS support
builder.Services.AddCors();

var dbPath = DatabasePathHelper.GetDatabasePath();
var connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContext<AcadesArchitecturePatternSQLiteContext>(x =>
{
    x.UseSqlite(connectionString);
});

Debug.WriteLine("Working Directory: " + Directory.GetCurrentDirectory());
Debug.WriteLine($"Connection String = {connectionString}");

/* x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); */

// Adding JWT authentication/validation 
builder.Services.AddAuthentication(options =>
{
    // Default authentication
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Validation parameters
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("AcadesArchitecturePattern-authentication-key")),
        ClockSkew = TimeSpan.FromMinutes(30),
        ValidIssuer = "AcadesArchitecturePattern",
        ValidAudience = "AcadesArchitecturePattern"
    };
});

// Redirecting HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5001;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore().AddControllersAsServices();

// Dependência e registros diversos
#region Sessions
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
});
#endregion

#region Users
builder.Services.AddTransient<IUserService, UserService>();

// Commands:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteUserHandle).Assembly));

// Queries:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ListUserHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetConfigurationHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchUserByEmailHandle).Assembly));
#endregion

#region ToDoList
builder.Services.AddTransient<IToDoListService, ToDoListService>();

// Commands:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateToDoListHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteToDoListHandle).Assembly));

// Queries:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ListToDoListHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchToDoListByIdHandle).Assembly));
#endregion

#region Task
builder.Services.AddTransient<ITaskService, TaskService>();

// Commands:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateTaskHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteTaskHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateTaskHandle).Assembly));

// Queries:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ListTaskHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchTaskByIdHandle).Assembly));
#endregion

#region Authentications
// Commands:
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginEmailHandle).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginUserNameHandle).Assembly));

// Security:
builder.Services.AddScoped<JwtTokenGenerator>();

// Services:
builder.Services.AddScoped<UserMappingService>();
builder.Services.AddScoped<AuthenticateTokenMappingService>();
#endregion

#region Events
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(TaskEventHandle).Assembly));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();