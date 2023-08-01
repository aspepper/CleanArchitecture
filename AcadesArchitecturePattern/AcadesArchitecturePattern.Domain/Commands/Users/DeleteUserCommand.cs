using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadesArchitecturePattern.Domain.Commands.Users
{
    public class DeleteUserCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public DeleteUserCommand() { }

        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                   .Requires()
                   .IsNotEmpty(Id, "Id", "O campo 'Id' não pode estar vazio!")
            );
        }
    }
}
