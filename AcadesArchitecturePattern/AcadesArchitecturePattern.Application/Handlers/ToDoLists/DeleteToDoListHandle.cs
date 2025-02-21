﻿using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.ToDoLists;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists
{
    public class DeleteToDoListHandle(IToDoListService toDoListService, ITaskService taskService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<DeleteToDoListCommand, GenericCommandResult>
    {
        private readonly IToDoListService toDoListService = toDoListService;
        private readonly ITaskService taskService = taskService;
        private readonly ILogger<ToDoListEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericCommandResult> Handle(DeleteToDoListCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Correctly inform the list you want to delete", command.Notifications));
                }

                var searchedToDoList = toDoListService.SearchById(command.Id);

                if (searchedToDoList == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "List not found", command.Notifications));
                }

                taskService.DeleteTasksByIdList(searchedToDoList.Id);

                toDoListService.Delete(searchedToDoList.Id);

                logger.LogInformation("List Completed: {CommandName}", command.GetType().Name);

                var listEvent = new ToDoListEvent(searchedToDoList);
                await mediator.Publish(listEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "List deleted successfully!", ""));
            }
            catch (Exception ex)
            {
                logger.LogInformation("List Failed: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "An error occurred while deleting the list", ex.Message));
            }
        }
    }
}
