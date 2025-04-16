using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using AcadesArchitecturePattern.Shared.Utils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Authentications;

public class LoginUserNameHandle(IUserService userService, ILogger<UserEventHandle> logger, IMediator mediator) : IRequestHandler<SearchUserByUserNameQuery, GenericQueryResult>
{
    private readonly IUserService userService = userService;
    private readonly ILogger<UserEventHandle> logger = logger;
    private readonly IMediator mediator = mediator;

    public async Task<GenericQueryResult> Handle(SearchUserByUserNameQuery query, CancellationToken cancellationToken)
    {
        try
        {
            query.Validate();

            if (!query.IsValid)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados do usuário", query.Notifications));
            }

            var searchedUser = userService.SearchByUserName(query.UserName);

            if (searchedUser == null)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Usuário ou Senha inválido", query.Notifications));
            }

            string encryptedPassword = PasswordEncryption.Encrypt(query.Password);

            if (searchedUser.Password != encryptedPassword)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Usuário ou Senha inválido", query.Notifications));
            }

            logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

            var userEvent = new UserEvent(searchedUser);
            await mediator.Publish(userEvent, cancellationToken);

            return await Task.FromResult(new GenericQueryResult(true, "Usuário logado!", searchedUser));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
            return await Task.FromResult(new GenericQueryResult(false, "Ocorreu um erro ao fazer Login.", ex.Message));
        }
    }
}