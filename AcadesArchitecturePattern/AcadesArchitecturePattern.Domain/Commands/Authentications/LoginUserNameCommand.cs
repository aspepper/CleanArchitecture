using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.Authentications
{
    public class LoginUserNameCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public LoginUserNameCommand(string company, string userName, string password)
        {
            Company = company;
            UserName = userName;
            Password = password;
        }

        public string Company { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

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
