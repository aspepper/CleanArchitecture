using AcadesArchitecturePattern.Domain.Commands.Authentications;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Utils;
using MediatR;

namespace AcadesArchitecturePattern.Application.Handlers.Authentications
{
    public class LoginEmailHandle : IRequestHandler<LoginEmailCommand, GenericCommandResult>
    {
        private readonly IUserService _userService;

        public LoginEmailHandle(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GenericCommandResult> Handle(LoginEmailCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    return await Task.FromResult(new GenericCommandResult(false, "Insira os dados corretamente", command.Notifications));
                }

                var searchedUser = _userService.SearchByEmail(command.Email);

                if (searchedUser == null)
                {
                    return await Task.FromResult(new GenericCommandResult(false, "E-mail ou senha inválidos", ""));
                }

                // Descriptografar a senha do usuário encontrado e comparar com a senha fornecida
                if (!PasswordEncryption.ValidateHashes(command.Password, searchedUser.Password))
                {
                    return await Task.FromResult(new GenericCommandResult(false, "E-mail ou senha inválidos", ""));
                }

                return await Task.FromResult(new GenericCommandResult(true, "Logado com sucesso!", searchedUser));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro durante o login com e-mail", ex.Message));
            }
        }
    }
}
