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
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public GetConfigurationHandle(ILogger<ToDoListEventHandle> logger, IMediator mediator, IConfiguration config)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = config;

            if (_client == null)
            {
                _client = new HttpClient()
                {
                    BaseAddress = new Uri($"{_configuration["APIConfigProtocol"]}://{_configuration["APIConfigServer"]}/{_configuration["APIConfigURL"]}/api/{_configuration["APIConfigVersion"]}/GetAdvConfig/")
                };
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<GenericQueryResult> Handle(GetConfigurationByCompanyQuery query, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"getConfig/{_configuration["APIConfigSystem"]}/{query.Company}", cancellationToken).Result;

                if (!response.IsSuccessStatusCode)
                { throw new Exception("Configuração não encontrada"); }

                var configuration = response.Content.ReadFromJsonAsync<Configuration>(cancellationToken: cancellationToken).Result ?? throw new Exception("Configuração não encontrada");
                var configurationEvent = new ConfigurationEvent(configuration);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                await _mediator.Publish(configurationEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "Configuração encontrada!", configurationEvent));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, ex.Message, query.Notifications));
            }
        }
    }
}
