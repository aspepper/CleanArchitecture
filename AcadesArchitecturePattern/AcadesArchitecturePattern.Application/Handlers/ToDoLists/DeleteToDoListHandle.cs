using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.ToDoLists;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists
{
    public class DeleteToDoListHandle : IRequestHandler<DeleteToDoListCommand, GenericCommandResult>
    {
        private readonly IToDoListService _toDoListService;
        private readonly ITaskService _taskService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public DeleteToDoListHandle(IToDoListService toDoListService, ITaskService taskService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _toDoListService = toDoListService;
            _taskService = taskService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(DeleteToDoListCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Correctly inform the list you want to delete", command.Notifications));
                }

                var searchedToDoList = _toDoListService.SearchById(command.Id);

                if (searchedToDoList == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "List not found", command.Notifications));
                }

                _taskService.DeleteTasksByIdList(searchedToDoList.Id);

                _toDoListService.Delete(searchedToDoList.Id);

                _logger.LogInformation("List Completed: {CommandName}", command.GetType().Name);

                var listEvent = new ToDoListEvent(searchedToDoList);
                await _mediator.Publish(listEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "List deleted successfully!", ""));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("List Failed: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "An error occurred while deleting the list", ex.Message));
            }
        }
    }
}
