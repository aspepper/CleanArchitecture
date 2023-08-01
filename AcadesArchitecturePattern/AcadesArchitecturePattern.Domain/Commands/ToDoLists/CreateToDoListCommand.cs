using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.ToDoLists
{
    public class CreateToDoListCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public CreateToDoListCommand() { }

        public CreateToDoListCommand(string title, EnColor color, Guid idUser)
        {
            Title = title;
            Color = color;
            IdUser = idUser;
        }

        public string Title { get; set; }
        public EnColor Color { get; set; } = EnColor.White;

        // FK's
        public Guid IdUser { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsNotEmpty(Title, "Title", "O campo 'Título' não pode estar vazio")
                .IsNotNull(Color, "Color", "O campo 'Cor' não pode ser nulo")
                .IsNotEmpty(IdUser, "IdUser", "O campo 'IdUser' não pode estar vazio")
                );
        }
    }
}
