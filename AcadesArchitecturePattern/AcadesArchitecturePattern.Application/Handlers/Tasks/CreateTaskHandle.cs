using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class CreateTaskHandle : IRequestHandler<CreateTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService _taskService;
        private readonly IToDoListService _listService;
        private readonly ILogger<TaskEventHandle> _logger;
        private readonly IMediator _mediator;

        public CreateTaskHandle(ITaskService taskService, IToDoListService listService, ILogger<TaskEventHandle> logger, IMediator mediator)
        {
            _taskService = taskService;
            _listService = listService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Insira corretamente os dados da tarefa", command.Notifications));
                }

                var listExists = _listService.SearchById(command.IdList);

                if (listExists == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Lista não encontrada", command.Notifications));
                }

                Domain.Entities.ToDoTask newTask = new Domain.Entities.ToDoTask(command.Name, command.Description, command.Priority, command.Status, command.Reminder, command.IdList);

                if (!newTask.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Dados de tarefa inválidos", newTask.Notifications));
                }

                _taskService.Add(newTask);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(newTask);
                await _mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa criada com sucesso!", newTask));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao criar a tarefa", ex.Message));
            }
        }
    }
}
