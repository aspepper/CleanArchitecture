using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class SearchUserByIdHandle : IRequestHandler<SearchUserByIdQuery, GenericQueryResult>
    {
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;
        private HttpClient _client;
        private IConfiguration _configuration;

        public SearchUserByIdHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator, IConfiguration config)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
            _configuration = config;

            if (_client == null)
            {
                _client = new HttpClient
                {
                    BaseAddress = new Uri($"{_configuration["APIConfigProtocol"]}://{_configuration["APIConfigServer"]}/{_configuration["APIConfigURL"]}/")
                };
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

        }

        public async Task<GenericQueryResult> Handle(SearchUserByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                query.Validate();

                if (!query.IsValid)
                {
                    return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados do usuário", query.Notifications));
                }

                HttpResponseMessage response = _client.GetAsync($"/{_configuration["APIConfigSystem"]}/{_configuration["APIConfigCompany"]}", cancellationToken).Result;

                if (!response.IsSuccessStatusCode)
                { throw new Exception("Usuário não encontrado"); }

                var searchedUser = response.Content.ReadFromJsonAsync<Domain.Entities.User>(cancellationToken: cancellationToken).Result ?? throw new Exception("Usuário não encontrado");

                _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                var userEvent = new UserEvent(searchedUser);
                await _mediator.Publish(userEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "Usuários encontrados!", searchedUser));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, ex.Message, query.GetType().Name));
            }
        }
    }
}
