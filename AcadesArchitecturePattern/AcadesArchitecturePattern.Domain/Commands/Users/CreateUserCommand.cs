    using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.Users
{
    public class CreateUserCommand(string userName, string email, string password) : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public string UserName { get; set; } = userName;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(UserName, "UserName", "O campo 'UserName' não pode estar vazio")
                .IsEmail(Email, "Email", "Digite um endereço de e-mail válido")
                .IsGreaterThan(Password, 6, "O campo 'Senha' deve ter no mínimo 6 caracteres")
            );
        }
    }
}
