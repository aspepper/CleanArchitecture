using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class GetConfigurationHandle : IRequestHandler<GetConfigurationByCompanyQuery, GenericQueryResult>
    {
        private readonly ILogger<ToDoListEventHandle> logger;
        private readonly IMediator mediator;
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        public GetConfigurationHandle(ILogger<ToDoListEventHandle> logger, IMediator mediator, IConfiguration config)
        {
            this.logger = logger;
            this.mediator = mediator;
            configuration = config;

            if (client == null)
            {
                client = new HttpClient()
                {
                    BaseAddress = new Uri($"{configuration["APIConfigProtocol"]}://{configuration["APIConfigServer"]}/{configuration["APIConfigURL"]}/api/{configuration["APIConfigVersion"]}/GetAdvConfig/")
                };
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<GenericQueryResult> Handle(GetConfigurationByCompanyQuery query, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"getConfig/{this.configuration["APIConfigSystem"]}/{query.Company}", cancellationToken).Result;

                if (!response.IsSuccessStatusCode)
                { throw new Exception("Configuração não encontrada"); }

                var configuration = response.Content.ReadFromJsonAsync<Configuration>(cancellationToken: cancellationToken).Result ?? throw new Exception("Configuração não encontrada");
                var configurationEvent = new ConfigurationEvent(configuration);

                logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                await mediator.Publish(configurationEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "Configuração encontrada!", configurationEvent));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, ex.Message, query.Notifications));
            }
        }
    }
}
