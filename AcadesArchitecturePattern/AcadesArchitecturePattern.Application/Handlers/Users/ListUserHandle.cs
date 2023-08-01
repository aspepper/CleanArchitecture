using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class ListUserHandle : IRequestHandler<ListUserQuery, GenericQueryResult>
    {
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public ListUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericQueryResult> Handle(ListUserQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var list = _userService.List();

                if (list.Any())
                {
                    _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                    // Trigger the UserEvent event
                    foreach (var user in list)
                    {
                        var userEvent = new UserEvent(user);
                        await _mediator.Publish(userEvent, cancellationToken);
                    }

                    return new GenericQueryResult(true, "Usuários encontrados!", list);
                }
                else
                {
                    return new GenericQueryResult(false, "Usuários não encontrados", list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar usuários", ex.Message));
            }
        }
    }
}
