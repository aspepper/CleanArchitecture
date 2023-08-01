using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.ToDoLists;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists
{
    public class SearchToDoListByIdHandle : IRequestHandler<SearchToDoListByIdQuery, GenericQueryResult>
    {
        private readonly IToDoListService _toDoListService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public SearchToDoListByIdHandle(IToDoListService toDoListService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _toDoListService = toDoListService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericQueryResult> Handle(SearchToDoListByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                query.Validate();

                if (!query.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return await Task.FromResult(new GenericQueryResult(false, "Correctly enter list data", query.Notifications));
                }

                var searchedToDoList = _toDoListService.SearchById(query.Id);

                if (searchedToDoList == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return await Task.FromResult(new GenericQueryResult(false, "List not found", query.Notifications));
                }

                _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                var listEvent = new ToDoListEvent(searchedToDoList);
                await _mediator.Publish(listEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "List found!", searchedToDoList));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "An error occurred while listing lists by id", ex.Message));
            }
        }
    }
}
