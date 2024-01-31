<h2 align="center">Acades Patterns</h2>

<p align="center">
  Solid-based API template for agile development of various Acades systems, providing essential features for faster and more efficient construction.
  <br />
  <a href="https://github.com/aspepper/CleanArchitecture"><strong> View Repository
  »</strong></a>
  <br />
  <br />
</p>

<br>

## Solution Map 🗺️

* [About the Template 🔍](#about)
* [Key Concepts 🔑](#keys)
* [Standards 🔒](#default)
* [General Architecture and Development Flow 🏰](#architecture)
* [Class and Folder Naming 📁](#nomenclatures)
* [About Projects in the Solution 📂](#projects)
* [Project Packages 📦](#packages)
* [Configuration and Deployment 👩‍💻](#configs)
* [Best Practices and Considerations 🧹](#habits)

<br>
<br>

<div id='about'/>

## About the Template 🔍
API of a to-do list system that offers a ***modern and scalable solution*** for managing personal tasks. Users can create, update, and delete lists (***CRUD***), as well as add and manipulate individual tasks.

The API architecture incorporates concepts such as ***CQRS***, ***Event Sourcing***, ***MediatR***, ***SAGA***, ***TDD***, ***DDD***, and ***Clean Code***, ensuring a solid and efficient structure.

The main goal is to provide users with an intuitive and effective experience for managing their daily activities.

<br>
<br>

<div id='keys'/>

## Key Concepts 🔑
Below are explanations of the key concepts mentioned above that underpin the to-do list system API:

* ***<ins>CQRS (Command Query Responsibility Segregation):***</ins> <br>
  It is an architectural pattern used in the API to separate read operations (***queries***) from write operations (***commands***). This allows ***better separation of concerns*** and ***performance optimization*** when dealing with different types of operations.

* ***<ins>Event Sourcing:***</ins> <br>
  The API uses the concept of events to ***notify*** and ***react to changes in the state of entities***. Events are emitted whenever a significant action occurs, such as ***creating a new to-do list*** or ***completing a task***. These events can be consumed by other system components to perform additional actions, such as sending notifications or updating other related entities.

* ***<ins>MediatR:***</ins> <br>
  It is a design pattern that enables ***communication*** and ***management of commands/queries and events*** among different components of the API. It facilitates the use of the CQRS pattern, allowing ***separation*** and ***proper handling of command/query and event requests***.

* ***<ins>SAGA Orchestration Pattern:***</ins> <br>
  Used to ***orchestrate*** and ***manage complex transactions*** and ***business processes*** involving multiple steps and components. In the API, SAGAs can be ***used to handle workflows involving multiple operations*** related to tasks and to-do lists, ensuring that these operations are executed ***consistently*** and ***reliably***.

* ***<ins>DDD (Domain-Driven Design):***</ins> <br>
  A software design approach that aims at ***efficient and cohesive modeling of the business domain***. The API follows DDD principles to organize entities, aggregates, services, and events in a clear and understandable domain structure, allowing for ***flexible and scalable design***.

* ***<ins>Clean Code:***</ins> <br>
  The API adopts Clean Code principles to promote ***readable***, ***concise***, and ***easily maintainable code***. This includes the use of meaningful naming for classes and methods, creating small and well-defined functions, eliminating code duplication, and adopting good programming practices.

* ***<ins>TDD (Test-Driven Development):***</ins> <br>
  A development approach that emphasizes ***creating unit tests*** before implementing the code. In the API, unit tests are used to ***verify the correctness*** and ***robustness of implemented features***. Tests ensure that the API is working as expected, providing ***greater reliability*** and ***facilitating code maintenance***.

<br>
<br>

<div id='default'/>

## Standards 🔒
The API adopts the following standards:

* ***<ins>Document Standardization:***</ins> <br>
  The API uses masks, sizes, and regular expressions to format document-related fields in a standardized way. This ensures that documents are inserted and displayed in a standardized manner.

* ***<ins>Type, Date, and Number Standardization:***</ins> <br>
  It uses the ISO/GNT format to standardize types, dates, and numbers. Additionally, the UTC standard is used for storing dates and times, ensuring consistency and interoperability.

* ***<ins>Screen Template:***</ins> <br>
  The API provides templates for screens that include data validation, formatting, and other user interface-related functionalities. This helps ensure a consistent and user-friendly experience for API users.

* ***<ins>API Template:***</ins> <br>
  The API uses standardized templates to ensure security, data validation, and other essential functionalities in its programming interfaces. This facilitates the development of new features and ensures API consistency.

* ***<ins>Database Access:***</ins> <br>
  EntityFramework is used to facilitate access and manipulation of data in the database. This allows efficient abstraction of database operations and improves development productivity.

* ***<ins>Log Handling:***</ins> <br>
  The API uses NLog for log and exception handling. This allows recording relevant information and identifying the origin of queries and operations performed in the API.

* ***<ins>Multi-language:***</ins> <br>
  Supports Portuguese, English, and Spanish languages, enabling application internationalization and catering to different audiences.

* ***<ins>LGPD (General Data Protection Law):***</ins> <br>
  The API complies with LGPD. It implements mechanisms to ensure data privacy and security, such as searching by name and document, which can be performed in parts, ensuring the protection of personal data.

<br>
<br>

<div id='architecture'/>

## General Architecture and Development Flow 🏰

The solution's architecture follows a modular approach, divided into different layers and components that fit together to provide the complete functionality of the API.

* _**Layer 0 - <ins>AcadesArchitecturePattern.Tests**_</ins>
  * Will be developed and implemented as the progress of all solution projects.
  * Contains unit tests for all entities, commands, queries, and handlers of the project.

<br>

* _**Layer 1 - <ins>AcadesArchitecturePattern.Shared**_</ins>

  * ***Entities:*** Contains the definition of base entities (Base) that can be extended by other entities.

  * ***Commands:*** Defines API commands, such as GenericCommandResult, ICommand, and ICommandResult, used to perform create, update, and delete operations.

  * ***Queries:*** Defines API queries, such as GenericQueryResult, IQuery, and IQueryResult, used to retrieve information from data.

  * ***Handlers:*** Defines contracts for command (IHandlerCommand) and query (IHandlerQuery) handlers.

  * ***Events:*** Defines base events (BaseEvent) that can be used to notify and react to changes in the system.

  * ***Enums:*** Defines enumerations, such as EnColor, EnStatusTask, and EnTaskPriorityLevel, used to represent different properties and states.

  * ***Utils:*** Contains utility implementations, such as PasswordEncryption, used for encrypting passwords.

<br>

* _**Layer 2 - <ins>AcadesArchitecturePattern.Domain**_</ins>

  * ***Entities:*** Defines domain-specific entities, such as User, ToDoList, and Task, representing the main objects of the system.

  * ***Commands:*** Defines commands related to each entity, such as CreateUserCommand, CreateToDoListCommand, etc.

  * ***Queries:*** Defines queries related to each entity, such as ListUserQuery, ListToDoListQuery, etc.

  * ***Events:*** Defines specific events for each entity, such as UserEvent, ToDoListEvent, etc.

  * ***Interfaces:*** Defines service interfaces (ITaskService, IToDoListService, IUserService) for entity manipulation.

<br>

* _**Layer 3 - <ins>AcadesArchitecturePattern.Infra.Data**_</ins>

  * ***Mappings:*** Contains mapping classes (TaskMapping, ToDoListMapping, UserMapping) to map domain entities to the database.

  * ***Contexts:*** Represents the desired database context (AcadesArchitecturePatternSqlServerContext) allowing data access.

  * ***Services:*** Provides implementation of services related to each entity, such as TaskService, ToDoListService, UserService.

<br>

* _**Layer 4 - <ins>AcadesArchitecturePattern.Application**_</ins>
  * ***Handlers:*** Implements handlers that deal with domain-specific commands and queries.

  * ***Security:*** Contains JwtTokenGenerator, responsible for generating JWT tokens for authentication.

  * ***Services:*** Provides implementation of specific services, such as UserMappingService.

<br>

* _**Layer 5 - <ins>AcadesArchitecturePattern.Api**_</ins>
  * ***Controllers:*** Contains controllers providing API endpoints for entity manipulation.

<br>
<br>

<div id='nomenclatures'/>

## Class and Folder Naming 📁

The naming convention follows guidelines to make the code structure more understandable and consistent. Here are some examples of naming with their meanings:

* _<ins>***Classes:***_</ins>
  * ***ClassName:*** Classes are named using the ***PascalCase*** pattern in the ***singular form***, following the convention of starting each word with an uppercase letter. Also, it is important to use names ***in English*** to adhere to the correct naming convention.
    * ***Examples:*** User, ToDoList, Task, CreateUserCommand, IToDoListService, SearchTaskByIdQuery.

<br>

* _<ins>***Folders:***_</ins>
  * ***FolderNames:*** Folders are named using the ***PascalCase*** pattern in the ***plural form***, following the convention of starting each word with an uppercase letter. Also, it is important to use names ***in English*** to adhere to the correct naming convention.
    * ***Examples:*** Entities, Commands, Queries, Controllers, Services, Contexts.

<br>

* _<ins>***Specific Class Naming:***_</ins>

  #### ***Commands:***

  * ***[Action][Entity]Command:*** Follows the pattern where ***[Action] is replaced by the action*** the Command will perform and where ***[Entity] is replaced by the related entity***.
    * ***Examples:*** CreateTaskCommand, DeleteUserCommand, UpdateToDoListCommand.

  <br>
  <br>

  #### ***Queries:***

  * ***List[Entity]Query:*** Follows the pattern where ***[Entity] is replaced by the related entity***.
    * ***Examples:*** ListTaskQuery, ListUserQuery, ListToDoListQuery.

  <br>

  * ***Search[Entity]By[Parameter]Query:*** Follows the pattern where ***[Entity] is replaced by the related entity*** and where ***[Parameter] is replaced by the name of the query parameter***.
    * ***Examples:*** SearchTaskByIdQuery, SearchUserByUserNameQuery, SearchUserByEmailQuery.

  <br>
  <br>

  #### ***Handlers:***

  * ***[Action][Entity]Handle:*** Follows the pattern where ***[Action] is replaced by the action*** the Handler will perform and where ***[Entity] is replaced by the related entity***. Finally, the word ***Handle*** is added, ***WITHOUT the letter "r"*** as it is written Handler.
    * ***Examples:*** CreateTaskHandle, DeleteUserHandle, UpdateToDoListHandle.

  <br>

  * ***Search[Entity]By[Parameter]Handle:*** Follows the pattern where ***[Entity] is replaced by the related entity*** and where ***[Parameter] is replaced by the name of the parameter***. Finally, the word ***Handle*** is added, ***WITHOUT the letter "r"*** as it is written Handler.
    * ***Examples:*** SearchTaskByIdHandle, SearchUserByUserNameHandle, SearchUserByEmailHandle.

  <br>
  <br>

  #### ***Interfaces:***

  * ***I[Entity]Service:*** Follows the pattern where ***accompanied by the letter "I" at the beginning*** and where ***[Entity] is replaced by the entity***.
    * ***Examples:*** ITaskService, IToDoListService, IUserService.

  <br>
  <br>

  #### ***Contexts:***

  * ***[SolutionName][DatabaseName]Context:*** Follows the pattern where ***[SolutionName] is replaced by the solution name*** and where ***[DatabaseName] is replaced by the name of the database to be used***.
    * ***Examples:*** AcadesArchitecturePatternSqlServerContext, AcadesArchitecturePatternOracleContext, AcadesArchitecturePatternMySqlContext.

  <br>
  <br>

  #### ***Mappings:***

  * ***[Entity]Mapping:*** Follows the pattern where ***[Entity] is replaced by the entity***.
    * ***Examples:***
#### ***Controllers:***

* ***[Entities]Controller:*** Follows the pattern, where ***[Entities] is replaced by the entity in the plural form***.
  * ***Examples:*** TasksController, ToDoListsController, UsersController. <br>

<br>
<br>

#### ***Tests:***

* ***[Entity]Test:*** Follows the pattern, where ***[Entity] is replaced by the entity***.
  * ***Examples:*** TaskTest, ToDoListTest, UserTest. <br>

<br>

* ***[CommandName]Test:*** Follows the pattern, where ***[CommandName] is replaced by the full name of the command*** related to the entity.
  * ***Examples:*** CreateTaskCommandTest, DeleteUserCommandTest, UpdateToDoListCommandTest. <br>

<br>

* ***[QueryName]Test:*** Follows the pattern, where ***[QueryName] is replaced by the full name of the query*** related to the entity.
  * ***Examples:*** ListTaskQueryTest, SearchUserByIdQueryTest, SearchUserByUserNameQueryTest. <br>

<br>

* ***[HandleName]Test:*** Follows the pattern, where ***[HandleName] is replaced by the full name of the handle*** related to the entity.
  * ***Examples:*** CreateTaskHandleTest, ListToDoListHandleTest, UpdateUserHandleTest. <br>

<br>
<br>

<div id='projects'/>

## About Projects in the Solution 📂
Projects present in the solution and a brief description of the purpose of each and their types:

* _<ins>***AcadesArchitecturePattern (Blank Solution):***_</ins> 
  * ***Description:*** Empty project solution for scalable systems development.
  * ***Purpose:*** Provide an organized and modular structure for software application development.

<br>

* _<ins>***AcadesArchitecturePattern.Shared (Class Library):***_</ins> 
  * ***Description:*** Contains shared classes and structures used throughout the solution.
  * ***Purpose:*** Provide common and reusable functionalities for other projects in the solution.

<br>

* _<ins>***AcadesArchitecturePattern.Domain (Class Library):***_</ins> 
  * ***Description:*** Contains the system's domain entities, such as User, ToDoList, and Task.
  * ***Purpose:*** Define domain entities and their business rules, encapsulating domain logic.

<br>

* _<ins>***AcadesArchitecturePattern.Infra.Data (Class Library):***_</ins> 
  * ***Description:*** Responsible for data access and persistence, containing mappings and database contexts.
  * ***Purpose:*** Implement the data access layer, interacting with databases and performing persistence operations.

<br>

* _<ins>***AcadesArchitecturePattern.Application (Class Library):***_</ins> 
  * ***Description:*** Implements handlers for commands and queries, as well as other application services.
  * ***Purpose:*** Manage application business logic, process commands and queries, and provide specific services.

<br>

* _<ins>***AcadesArchitecturePattern.Api (Application Project):***_</ins> 
  * ***Description:*** Contains RESTful API controllers, which receive HTTP requests and provide corresponding responses.
  * ***Purpose:*** Expose API endpoints for interaction with external clients, handling communication and presentation logic.

<br>

* _<ins>***AcadesArchitecturePattern.Tests (Test Project):***_</ins> 
  * ***Description:*** Contains unit tests for entities, commands, queries, and handlers of the application.
  * ***Purpose:*** Verify the correct implementation of functionalities, ensure code quality, and prevent regressions.

<br>
<br>

<div id='packages'/>

## Packages in Projects 📦
Packages present in the projects and a brief description of the purpose of each:

* _**Project - <ins>AcadesArchitecturePattern.Shared**_</ins> 
  * ***BCrypt.Net-Core (version 1.6.0):*** Library that provides support for password hashing using the BCrypt algorithm. <br>

  <br>

  * ***Flunt (version 2.0.5):*** Library that provides support for object validation and error notifications. <br>

  <br>

  * ***MediatR (version 12.0.1):*** Library that implements the Mediator pattern for communication between different components of an application. <br>

<br>
<br>

* _**Project - <ins>AcadesArchitecturePattern.Domain**_</ins> 
  * ***Acades.Abstractions (version 2023.7.3.926-alpha):*** Library containing common abstractions and interfaces used in CQRS (Command Query Responsibility Segregation) and Event Sourcing-based architectures. <br>

  <br>

  * ***Acades.Saga (version 2023.7.3.957-alpha):*** Library that provides support for implementing the Saga design pattern in event-driven architectures. <br>

  <br>

  * ***MediatR (version 12.0.1):*** Library that implements the Mediator pattern for communication between different components of an application. <br>

  <br>

  * ***MediatR.Extensions.Microsoft.DependencyInjection (version 5.1.2):*** Provides support for dependency injection in ASP.NET Core. <br>

  <br>

  * ***Microsoft.Extensions.DependencyModel (version 7.0.0):*** Provides resources for accessing information about runtime dependencies. <br>

  <br>

  * ***Microsoft.Extensions.Logging (version 7.0.0):*** Provides logging capabilities in ASP.NET Core. <br>

  <br>

  * ***Microsoft.Extensions.Logging.Abstractions (version 7.0.1):*** Contains abstractions for logging features in ASP.NET Core. <br>

  <br>

  * ***Microsoft.Extensions.Logging.Debug (version 7.0.0):*** Provides a log provider that writes messages to the debugger during development. <br>

  <br>

  * ***Scrutor (version 4.2.2):*** Library that simplifies service registration based on conventions using ASP.NET Core dependency injection. <br>

<br>
<br>

* _**Project - <ins>AcadesArchitecturePattern.Infra.Data**_</ins> 
  * ***Microsoft.EntityFrameworkCore (version 7.0.5):*** Provides data access and object-relational mapping features for Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Design (version 7.0.5):*** Provides support for code generation and design tools for Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Relational (version 7.0.5):*** Provides support for additional relational features in Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.SqlServer (version 7.0.5):*** Provides specific support for using SQL Server in Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Tools (version 7.0.5):*** Provides additional tools for Entity Framework Core, such as database migrations. <br>

<br>
<br>

* _**Project - <ins>AcadesArchitecturePattern.Application**_</ins> 
  * ***FluentValidation.DependencyInjectionExtensions (version 11.5.2):*** Provides support for integrating FluentValidation with ASP.NET Core dependency injection. <br>

  <br>

  * ***MediatR (version 12.0.1):*** Library that implements the Mediator pattern for communication between different components of an application. <br>

  <br>

  * ***MediatR.Extensions.Microsoft.DependencyInjectionFixed (version 5.1.2):*** Provides support for integrating MediatR with ASP.NET Core dependency injection. <br>

  <br>

  * ***Microsoft.AspNetCore.Authentication.JwtBearer (version 7.0.5):*** Provides support for JWT (JSON Web Token) based authentication in ASP.NET Core. <br>

<br>
<br>

* _**Project - <ins>AcadesArchitecturePattern.Api**_</ins> 
  * ***Microsoft.AspNetCore.Mvc.NewtonsoftJson (version 6.0.19):*** Provides support for custom serialization and deserialization using the Newtonsoft.Json library in ASP.NET Core MVC. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore (version 7.0.5):*** Provides data access and object-relational mapping features for Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Design (version 7.0.5):*** Provides support for code generation and design tools for Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Relational (version 7.0.5):*** Provides support for additional relational features in Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.SqlServer (version 7.0.5):*** Provides specific support for using SQL Server in Entity Framework Core. <br>

  <br>

  * ***Microsoft.EntityFrameworkCore.Tools (version 7.0.5):*** Provides additional tools for Entity Framework Core, such as database migrations. <br>

  <br>

  * ***Newtonsoft.Json (version 13.0.3):*** A popular JSON serialization and deserialization library. <br>

  <br>

  * ***Swashbuckle.AspNetCore (version 6.5.0):*** Provides support for generating interactive API documentation using Swagger/OpenAPI in ASP.NET Core. <br>

<br>
<br>

* _**Project - <ins>AcadesArchitecturePattern.Tests**_</ins> 
  * ***FluentAssertions (version 6.11.0):*** Library that provides a fluent API for writing assertions in unit tests. <br>

  <br>

  * ***Microsoft.NET.Test.Sdk (version 17.5.0):*** Provides support for running .NET Core tests. <br>

  <br>

  * ***Moq (version 4.18.4):*** Library that allows the creation of mock objects for unit testing. <br>

  <br>

  * ***xUnit (version 2.4.2):*** Unit testing framework for .NET. <br>

<br>
<br>

<div id='configs'/>

## Configuration and Deployment 👩‍💻
To set up and deploy the API in different environments, such as development, testing, and production, you can follow the instructions below:

* _<ins>***General Configuration:***_</ins> 
  * ***Infrastructure:*** Windows 2016. <br>

  * ***Databases:*** SQL Server 2019, Oracle 19G, MySQL 8.0, or 5.7. <br>

  * ***SDK and Entity Framework Version:*** Make sure you have the correct versions of .NET 7 SDK and Entity Framework Core 7 installed on your development machine. There will be a migration to version 8 in November 2023, with a deadline until May 2024. <br>

  * ***Cloud Binary Storage:*** Amazon S3 or Azure Storage for SaaS and OnPremise. <br>

  * ***Source Repository:*** Azure DevOps + Git. <br>

  * ***Adding Services:*** Code lines related to adding services (***builder.Services.AddControllers***, ***builder.Services.AddSwaggerGen***) are part of the general configuration. They set up the necessary services for the application to work, such as ***route control***, ***JSON serialization***, ***Swagger documentation***, among others. <br>
    * Adding services to the container:
    
      ```
      builder.Services.AddControllers()
      .AddNewtonsoftJson(options =>
      {
          options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });
      ```

    * Connecting to the database:
    
      ```
      builder.Services.AddDbContext<AcadesArchitecturePatternSqlServerContext>(x =>
      {
          x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });
      ```

    * Dependency injections:
    
      ```
      #region Users
        builder.Services.AddTransient<IUserService, UserService>();

        // Commands:
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserHandle).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteUserHandle).Assembly));

        // Queries:
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ListUserHandle).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchUserByIdHandle).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchUserByEmailHandle).Assembly));
      #endregion
      ```

  * ***Adding JWT Authorization:*** Code lines related to JWT authentication (***builder.Services.AddAuthentication*** and ***builder.Services.AddJwtBearer***) are also part of the general configuration. They configure ***JWT authentication*** by setting the parameters for ***JWT token validation***. <br>

    * Adding JWT authentication/validation:
    
      ```
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
      ```

* _<ins>***Development Environment:***_</ins> 
  * Ensure you have a database server available for use in the development environment. This can be SQL Server Express or LocalDB, depending on your choice. <br>

  * Open the ***appsettings.json*** or ***appsettings.Development.json*** file in the ***AcadesArchitecturePattern.Api*** project. <br>

  * Check the connection string named "***DefaultConnection***". Make sure it is correct for the development environment. <br>

    * Example connection in ***appsettings.json*** or ***appsettings.Development.json*** for ***SQL Server Express***: <br>
      ```
      {
        "Logging": {
          "LogLevel": {
            "Default": "Information",
            "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
          }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
          // SQL Server Connection
          "DefaultConnection": "Server=.\\SQLEXPRESS; Database=AcadesArchitecturePatternDb; Integrated Security=True; TrustServerCertificate=True"
        }
      }
      ```

    * Example connection in ***appsettings.json*** or ***appsettings.Development.json*** for ***LocalDB***: <br>
      ```
      {
        "Logging": {
          "LogLevel": {
            "Default": "Information",
            "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
          }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
          // SQL Server Connection
          "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AcadesArchitecturePatternDb;Trusted_Connection=True;MultipleActiveResultSets=true"
        }
      }
      ```

  * Ensure that other configurations in the ***appsettings.json*** or ***appsettings.Development.json*** file are adjusted for the development environment. <br>

  * ***Adding Swagger:*** Code lines related to Swagger (***builder.Services.AddSwaggerGen***, ***app.UseSwagger***, ***app.UseSwaggerUI***) are commonly used in the development environment to document and test the API. They provide an interactive interface to explore and test API endpoints. <br>  
    * Adding authorization to Swagger:

      ```
      builder.Services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "AcadesArchitecturePattern.Api", Version = "v1" });

          // Defining Swagger security definition for Bearer authentication
          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
          {
              Name = "Authorization",
              Type = SecuritySchemeType.ApiKey,
              Scheme = "Bearer",
              BearerFormat = "JWT",
              In = ParameterLocation.Header,
              Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
          });

          // Requiring Bearer security for Swagger operations
          c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              {
                  new OpenApiSecurityScheme
                  {
                      Reference = new OpenApiReference
                      {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                      }
                  },
                  new string[] {}
              }
          });
      });
      ```

    * Configuring the HTTP request pipeline:
    
      ```
      app.UseSwagger();
      app.UseSwaggerUI();
      ```

* _<ins>***Starting Migration and Creating the Database through Code First:***_</ins> 
  * Ensure you have a suitable database server configured and available in the production environment, such as a dedicated SQL server or a managed database service.

  <br>

  * _<ins>***Setting Up Cloud Binary Storage:***_</ins> 
  * If you're using cloud binary storage, ensure you have the necessary credentials and connection strings for the chosen service (Amazon S3 or Azure Storage). Update the relevant settings in the ***appsettings.json*** or ***appsettings.Production.json*** file.

  <br>

  * _<ins>***Finalizing Deployment:***_</ins> 
  * Deploy the application to the production environment using the chosen deployment method (Azure DevOps, manual deployment, etc.).

  <br>

  * _<ins>***Monitoring and Maintenance:***_</ins> 
  * Implement monitoring solutions and regularly check logs to ensure the health and performance of the application in the production environment.

  <br>

  * _<ins>***Updating Environment-Specific Configurations:***_</ins> 
  * If needed, update environment-specific configurations in the ***appsettings.json*** or ***appsettings.Production.json*** file.

  <br>

  * _<ins>***Scaling:***_</ins> 
  * Implement scaling solutions as needed based on the application's demand and usage patterns.

  <br>

  * _<ins>***Securing Production Environment:***_</ins> 
  * Implement security best practices, including firewall configurations, encryption, and access controls, to secure the production environment.

  <br>

  * _<ins>***Backup and Disaster Recovery:***_</ins> 
  * Set up regular backups and implement a disaster recovery plan to ensure data integrity and availability in case of unexpected events.

  <br>

  * _<ins>***Updating Dependencies:***_</ins> 
  * Regularly update dependencies, including libraries, frameworks, and SDKs, to benefit from the latest features and security patches.

  <br>

  * _<ins>***Documentation:***_</ins> 
  * Keep the documentation up-to-date, including API documentation, infrastructure configurations, and deployment procedures.

  <br>

  * _<ins>***Continuous Improvement:***_</ins> 
  * Continuously evaluate and improve the application, infrastructure, and deployment processes based on feedback, performance metrics, and evolving requirements.

  <br>

  * _<ins>***Collaboration:***_</ins> 
  * Foster collaboration between development, operations, and other relevant teams to ensure a smooth and efficient deployment process.

  <br>

  * _<ins>***Training and Knowledge Sharing:***_</ins> 
  * Provide training and knowledge sharing sessions for the team members involved in the deployment process to enhance their skills and awareness.

  <br>

  * _<ins>***Conclusion:***_</ins> 
  * By following these guidelines and best practices, you can ensure a successful deployment of the AcadesArchitecturePattern API across different environments. Regularly review and update the deployment process to align with industry standards and evolving technologies.
