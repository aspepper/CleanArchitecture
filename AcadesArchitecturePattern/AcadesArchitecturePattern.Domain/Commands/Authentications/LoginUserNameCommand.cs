using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.Authentications
{
    public class LoginUserNameCommand(string company, string userName, string password) : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public string Company { get; set; } = company;
        public string UserName { get; set; } = userName;
        public string Password { get; set; } = password;

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNull(Company, "Company", "O campo 'Company' não pode ser nulo")
                .IsNotNull(UserName, "UserName", "O campo 'UserName' não pode ser nulo")
                .IsGreaterThan(Password, 2, "O campo 'Senha' deve ter no mínimo 2 caracteres")
            );
        }
    }
}
