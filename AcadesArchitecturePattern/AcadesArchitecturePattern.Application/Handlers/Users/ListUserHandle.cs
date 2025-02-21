using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class ListUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<ListUserQuery, GenericQueryResult>
    {
        private readonly IUserService userService = userService;
        private readonly ILogger<ToDoListEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericQueryResult> Handle(ListUserQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var list = userService.List();

                if (list.Any())
                {
                    logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                    // Trigger the UserEvent event
                    foreach (var user in list)
                    {
                        var userEvent = new UserEvent(user);
                        await mediator.Publish(userEvent, cancellationToken);
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
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar usuários", ex.Message));
            }
        }
    }
}
