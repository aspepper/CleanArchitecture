using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.ToDoLists;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists
{
    public class ListToDoListHandle(IToDoListService toDoListService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<ListToDoListQuery, GenericQueryResult>
    {
        private readonly IToDoListService toDoListService = toDoListService;
        private readonly ILogger<ToDoListEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericQueryResult> Handle(ListToDoListQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var list = toDoListService.List();

                if (list.Any())
                {
                    logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                    // Trigger the ToDoListEvent event
                    foreach (var toDoList in list)
                    {
                        var toDoListEvent = new ToDoListEvent(toDoList);
                        await mediator.Publish(toDoListEvent, cancellationToken);
                    }

                    return new GenericQueryResult(true, "Lists found!", list);
                }
                else
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return new GenericQueryResult(false, "Lists not found", list);
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "An error occurred while listing lists", ex.Message));
            }
        }
    }
}
