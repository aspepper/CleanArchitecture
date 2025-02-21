using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.ToDoLists;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists;

public class CreateToDoListHandle(IToDoListService toDoListService, IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<CreateToDoListCommand, GenericCommandResult>
{
    private readonly IToDoListService toDoListService = toDoListService;
    private readonly IUserService userService = userService;
    private readonly ILogger<ToDoListEventHandle> logger = logger;
    private readonly IMediator mediator = mediator;

    public async Task<GenericCommandResult> Handle(CreateToDoListCommand command, CancellationToken cancellationToken)
    {
        try
        {
            command.Validate();

            if (!command.IsValid)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Correctly enter list data", command.Notifications));
            }

            var userExists = userService.SearchById(command.IdUser);

            if (userExists == null)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "User not found", command.Notifications));
            }

            ToDoList newToDoList = new ToDoList(command.Title, command.Color, command.IdUser);

            if (!newToDoList.IsValid)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Invalid list data", newToDoList.Notifications));
            }

            toDoListService.Add(newToDoList);

            logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

            var listEvent = new ToDoListEvent(newToDoList);
            await mediator.Publish(listEvent, cancellationToken);

            return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(true, "List created successfully!", newToDoList));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
            return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "An error occurred while creating the list", ex.Message));
        }
        
    }
}
