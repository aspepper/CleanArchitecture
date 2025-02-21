using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class CreateTaskHandle(ITaskService taskService, IToDoListService listService, ILogger<TaskEventHandle> logger, IMediator mediator) : IRequestHandler<CreateTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService taskService = taskService;
        private readonly IToDoListService listService = listService;
        private readonly ILogger<TaskEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericCommandResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Insira corretamente os dados da tarefa", command.Notifications));
                }

                var listExists = listService.SearchById(command.IdList);

                if (listExists == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Lista não encontrada", command.Notifications));
                }

                Domain.Entities.ToDoTask newTask = new Domain.Entities.ToDoTask(command.Name, command.Description, command.Priority, command.Status, command.Reminder, command.IdList);

                if (!newTask.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Dados de tarefa inválidos", newTask.Notifications));
                }

                taskService.Add(newTask);

                logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(newTask);
                await mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa criada com sucesso!", newTask));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao criar a tarefa", ex.Message));
            }
        }
    }
}
