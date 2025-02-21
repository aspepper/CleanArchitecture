using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class UpdateTaskHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator) : IRequestHandler<UpdateTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService taskService = taskService;
        private readonly ILogger<TaskEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericCommandResult> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Insira os dados corretamente", command.Notifications));
                }

                Domain.Entities.ToDoTask oldTask = taskService.SearchById(command.Id);

                if (oldTask == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Não há nenhuma tarefa com este id", command.Notifications));
                }

                oldTask.UpdateTask(command.Name, command.Description, command.Priority, command.Status, command.Reminder);

                taskService.Update(oldTask);

                logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(oldTask);
                await mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa alterada com sucesso!", oldTask));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao atualizar a tarefa", ex.Message));
            }
        }
    }
}
