using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.ToDoLists;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Handlers.Contracts;
using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class SearchUserByEmailHandle : IRequestHandler<SearchUserByEmailQuery, GenericQueryResult>
    {
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public SearchUserByEmailHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericQueryResult> Handle(SearchUserByEmailQuery query, CancellationToken cancellationToken)
        {
            try
            {
                query.Validate();

                if (!query.IsValid)
                {
                    return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados do usuário", query.Notifications));
                }

                var searchedUser = _userService.SearchByEmail(query.Email);

                if (searchedUser == null)
                {
                    return await Task.FromResult(new GenericQueryResult(false, "Usuário não encontrado", query.Notifications));
                }

                _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                var userEvent = new UserEvent(searchedUser);
                await _mediator.Publish(userEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "Usuários encontrados!", searchedUser));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar usuários por e-mail", ex.Message));
            }
        }
    }
}
