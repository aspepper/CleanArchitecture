using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Users;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Utils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users;

public class CreateUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator) : IRequestHandler<CreateUserCommand, GenericCommandResult>
{
    private readonly IUserService userService = userService;
    private readonly ILogger<ToDoListEventHandle> logger = logger;
    private readonly IMediator mediator = mediator;

    public async Task<GenericCommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            command.Validate();

            if (!command.IsValid)
            {
                return await Task.FromResult(new GenericCommandResult(false, "Insira corretamente os dados do usuário", command.Notifications));
            }

            var emailExists = userService.SearchByEmail(command.Email.ToLower());
            var userNameExists = userService.SearchByUserName(command.UserName.ToLower());

            if (emailExists != null)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "E-mail existente", "Insira outro e-mail"));
            }
            else if (userNameExists != null)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Nome de usuário existente", "Insira outro nome de usuário"));
            }

            bool spacesUserName = command.UserName.Contains(" ");

            if (spacesUserName)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Nome de usuário não pode ter espaços", "Insira outro nome de usuário"));
            }

            string encryptedPassword = PasswordEncryption.Encrypt(command.Password);

            User newUser = new(command.UserName.ToLower(), command.Email.ToLower(), encryptedPassword);

            if (!newUser.IsValid)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Dados de usuário inválidos", newUser.Notifications));
            }

            userService.Add(newUser);

            logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

            var userEvent = new UserEvent(newUser);
            await mediator.Publish(userEvent, cancellationToken);

            return await Task.FromResult(new GenericCommandResult(true, "Usuário criado com sucesso!", newUser));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
            return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao criar o usuário", ex.Message));
        }
        
    }
}
