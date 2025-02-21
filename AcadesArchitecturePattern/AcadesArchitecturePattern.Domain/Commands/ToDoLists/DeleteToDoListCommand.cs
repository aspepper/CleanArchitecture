using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.ToDoLists
{
    public class DeleteToDoListCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public DeleteToDoListCommand() { }

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
