using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class DeleteTaskHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator) : IRequestHandler<DeleteTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService taskService = taskService;
        private readonly ILogger<TaskEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericCommandResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Informe corretamente a tarefa que deseja excluir", command.Notifications));
                }

                var searchedTask = taskService.SearchById(command.Id);

                if (searchedTask == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Tarefa não encontrada", command.Notifications));
                }

                taskService.Delete(searchedTask.Id);

                // Log the request details
                logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(searchedTask);
                await mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa excluída com sucesso!", ""));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao excluir a tarefa", ex.Message));
            }
        }
    }
}
