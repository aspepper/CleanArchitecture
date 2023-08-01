using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Tasks;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class ListTaskHandle : IRequestHandler<ListTaskQuery, GenericQueryResult>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskEventHandle> _logger;
        private readonly IMediator _mediator;

        public ListTaskHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator)
        {
            _taskService = taskService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericQueryResult> Handle(ListTaskQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var list = _taskService.List();

                if (list.Any())
                {
                    // Log the request details
                    _logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                    // Trigger the TaskEvent event
                    foreach (var task in list)
                    {
                        var taskEvent = new TaskEvent(task);
                        await _mediator.Publish(taskEvent, cancellationToken);
                    }

                    return new GenericQueryResult(true, "Tarefas encontradas!", list);
                }
                else
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return new GenericQueryResult(false, "Tarefas não encontradas", list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar as tarefas", ex.Message));
            }
        }
    }
}
