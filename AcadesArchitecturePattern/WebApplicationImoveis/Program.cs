using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using WebApplicationImoveis;
using WebApplicationImoveis.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blazored.LocalStorage;

// Inicializações Básicos do Blazor
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Adi��o de Features Elementares
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");
builder.Services.AddBlazoredLocalStorage();

// Injeção dos Serviços Padrões do Template
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();


// Configuração do JWT para aplicar o Authentication e Authorization no front
builder.Services.AddHttpClient("WebAPI",
        client => client.BaseAddress = new Uri("https://localhost:7246/"))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("WebAPI"));

// Criação de Todos os Objetos
var host = builder.Build();

// Implementação da Estrutura para Multi Idiomas
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("AcadesComplianceCurrentCulture.get");
CultureInfo culture;
if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("pt-BR");
    await jsInterop.InvokeVoidAsync("AcadesComplianceCurrentCulture.set", "pt-BR");
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// Carga das Configurações 
host.Services.GetRequiredService<IConfigurationService>()?.Get();

// Execução da Aplicação
await host.RunAsync();

/***************************************************************************************
  -------------------------------     Documents     -----------------------------------
    https://www.puresourcecode.com/dotnet/blazor/svg-icons-and-flags-for-blazor/
    https://www.matblazor.com/
***************************************************************************************/
