using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.ToDoLists;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists;

public class SearchToDoListByIdHandle(IToDoListService toDoListService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<SearchToDoListByIdQuery, GenericQueryResult>
{
    private readonly IToDoListService toDoListService = toDoListService;
    private readonly ILogger<ToDoListEventHandle> logger = logger;
    private readonly IMediator mediator = mediator;

    public async Task<GenericQueryResult> Handle(SearchToDoListByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            query.Validate();

            if (!query.IsValid)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Correctly enter list data", query.Notifications));
            }

            var searchedToDoList = toDoListService.SearchById(query.Id);

            if (searchedToDoList == null)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "List not found", query.Notifications));
            }

            logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

            var listEvent = new ToDoListEvent(searchedToDoList);
            await mediator.Publish(listEvent, cancellationToken);

            return await Task.FromResult(new GenericQueryResult(true, "List found!", searchedToDoList));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
            return await Task.FromResult(new GenericQueryResult(false, "An error occurred while listing lists by id", ex.Message));
        }
    }
}
