using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Commands.ToDoLists;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Handlers.Contracts;
using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.ToDoLists
{
    public class CreateToDoListHandle : IRequestHandler<CreateToDoListCommand, GenericCommandResult>
    {
        private readonly IToDoListService _toDoListService;
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public CreateToDoListHandle(IToDoListService toDoListService, IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _toDoListService = toDoListService;
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(CreateToDoListCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Correctly enter list data", command.Notifications));
                }

                var userExists = _userService.SearchById(command.IdUser);

                if (userExists == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "User not found", command.Notifications));
                }

                ToDoList newToDoList = new ToDoList(command.Title, command.Color, command.IdUser);

                if (!newToDoList.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Invalid list data", newToDoList.Notifications));
                }

                _toDoListService.Add(newToDoList);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var listEvent = new ToDoListEvent(newToDoList);
                await _mediator.Publish(listEvent, cancellationToken);

                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(true, "List created successfully!", newToDoList));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "An error occurred while creating the list", ex.Message));
            }
            
        }
    }
}
