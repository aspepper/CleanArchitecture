using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users;

public class SearchUserByEmailHandle(IUserService userService, ILogger<UserEventHandle> logger, IMediator mediator) : IRequestHandler<SearchUserByEmailQuery, GenericQueryResult>
{
    private readonly IUserService userService = userService;
    private readonly ILogger<UserEventHandle> logger = logger;
    private readonly IMediator mediator = mediator;

    public async Task<GenericQueryResult> Handle(SearchUserByEmailQuery query, CancellationToken cancellationToken)
    {
        try
        {
            query.Validate();

            if (!query.IsValid)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados do usuário", query.Notifications));
            }

            var searchedUser = userService.SearchByEmail(query.Email);

            if (searchedUser == null)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Usuário não encontrado", query.Notifications));
            }

            logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

            var userEvent = new UserEvent(searchedUser);
            await mediator.Publish(userEvent, cancellationToken);

            return await Task.FromResult(new GenericQueryResult(true, "Usuários encontrados!", searchedUser));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
            return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao listar usuários por e-mail", ex.Message));
        }
    }
}
