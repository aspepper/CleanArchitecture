using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class DeleteTaskHandle : IRequestHandler<DeleteTaskCommand, GenericCommandResult>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskEventHandle> _logger;
        private readonly IMediator _mediator;

        public DeleteTaskHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator)
        {
            _taskService = taskService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Informe corretamente a tarefa que deseja excluir", command.Notifications));
                }

                var searchedTask = _taskService.SearchById(command.Id);

                if (searchedTask == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Tarefa não encontrada", command.Notifications));
                }

                _taskService.Delete(searchedTask.Id);

                // Log the request details
                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var taskEvent = new TaskEvent(searchedTask);
                await _mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Tarefa excluída com sucesso!", ""));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao excluir a tarefa", ex.Message));
            }
        }
    }
}
