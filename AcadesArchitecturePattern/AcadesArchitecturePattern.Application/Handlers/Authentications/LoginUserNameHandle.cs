using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Authentications;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AcadesArchitecturePattern.Application.Handlers.Authentications
{
    public class LoginUserNameHandle : IRequestHandler<LoginUserNameCommand, GenericCommandResult>
    {
        private readonly ILogger<UserEventHandle> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpClient _client;

        public LoginUserNameHandle(ILogger<UserEventHandle> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;

            var session = _httpContextAccessor?.HttpContext?.Session;
            string? advLinkWebApi = session?.GetString("AdvLinkWebApi");

            if(string.IsNullOrEmpty(advLinkWebApi)) { throw new Exception("Configuração não foi carregada"); }
            _client = new HttpClient()
            {
                BaseAddress = new Uri($"{advLinkWebApi}/Corp/")
            };
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GenericCommandResult> Handle(LoginUserNameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    return await Task.FromResult(new GenericCommandResult(false, "Insira usuário e senha", command.Notifications));
                }

                var data = new
                {
                    empresa = command.Company,
                    usuario = command.UserName,
                    senha = command.Password
                };

                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                Debug.WriteLine($"jsonData = {jsonData}");

                HttpResponseMessage response = _client.PostAsJsonAsync($"authenticate", data, cancellationToken).Result;

                if (!response.IsSuccessStatusCode)
                { throw new Exception("Usuário ou senha inválida"); }

                var authenticateToken = response.Content.ReadFromJsonAsync<AuthenticateResponse>(cancellationToken: cancellationToken).Result ?? throw new Exception("Token não foi gerado");
                var authenticateEvent = new AuthenticateEvent(authenticateToken);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                await _mediator.Publish(authenticateEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Logado com sucesso!", authenticateToken));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro durante o login com userName", ex.Message));
            }
        }
    }
}
