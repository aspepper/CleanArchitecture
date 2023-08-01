using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Users;
using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Utils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class CreateUserHandle : IRequestHandler<CreateUserCommand, GenericCommandResult>
    {
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public CreateUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<GenericCommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Insira corretamente os dados do usuário", command.Notifications));
                }

                var emailExists = _userService.SearchByEmail(command.Email.ToLower());
                var userNameExists = _userService.SearchByUserName(command.UserName.ToLower());

                if (emailExists != null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "E-mail existente", "Insira outro e-mail"));
                }
                else if (userNameExists != null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Nome de usuário existente", "Insira outro nome de usuário"));
                }

                bool spacesUserName = command.UserName.Contains(" ");

                if (spacesUserName)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Nome de usuário não pode ter espaços", "Insira outro nome de usuário"));
                }

                string encryptedPassword = PasswordEncryption.Encrypt(command.Password);

                User newUser = new User(command.UserName.ToLower(), command.Email.ToLower(), encryptedPassword);

                if (!newUser.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Dados de usuário inválidos", newUser.Notifications));
                }

                _userService.Add(newUser);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var userEvent = new UserEvent(newUser);
                await _mediator.Publish(userEvent, cancellationToken);

                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(true, "Usuário criado com sucesso!", newUser));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await System.Threading.Tasks.Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao criar o usuário", ex.Message));
            }
            
        }
    }
}
