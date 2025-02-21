using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Tasks;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Tasks
{
    public class SearchTaskByIdHandle(ITaskService taskService, ILogger<TaskEventHandle> logger, IMediator mediator) : IRequestHandler<SearchTaskByIdQuery, GenericQueryResult>
    {
        private readonly ITaskService taskService = taskService;
        private readonly ILogger<TaskEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericQueryResult> Handle(SearchTaskByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                query.Validate();

                if (!query.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados da tarefa", query.Notifications));
                }

                var searchedTask = taskService.SearchById(query.Id);

                if (searchedTask == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                    return await Task.FromResult(new GenericQueryResult(false, "Tarefa não encontrada", query.Notifications));
                }

                logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

                var taskEvent = new TaskEvent(searchedTask);
                await mediator.Publish(taskEvent, cancellationToken);

                return await Task.FromResult(new GenericQueryResult(true, "Tarefa encontrada!", searchedTask));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
                return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar tarefas por id", ex.Message));
            }
            
        }
    }
}
