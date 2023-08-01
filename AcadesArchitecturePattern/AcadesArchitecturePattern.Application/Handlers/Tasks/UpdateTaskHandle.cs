using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class UpdateTaskHandle : IRequestHandler<UpdateTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskEventHandle> _logger;
        private readonly IMediator _mediator;

        public UpdateTaskHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator)
        {
            _taskService = taskService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Insira os dados corretamente", command.Notifications));
                }

                Domain.Entities.ToDoTask oldTask = _taskService.SearchById(command.Id);

                if (oldTask == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Não há nenhuma tarefa com este id", command.Notifications));
                }

                oldTask.UpdateTask(command.Name, command.Description, command.Priority, command.Status, command.Reminder);

                _taskService.Update(oldTask);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(oldTask);
                await _mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa alterada com sucesso!", oldTask));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao atualizar a tarefa", ex.Message));
            }
        }
    }
}
