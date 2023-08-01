using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.Tasks
{
    public class CreateTaskCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public CreateTaskCommand() { }

        public CreateTaskCommand(string name, string description, EnTaskPriorityLevel priority, EnStatusTask status, DateTime? reminder, Guid idList)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
            Reminder = reminder;
            IdList = idList;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public EnTaskPriorityLevel Priority { get; set; }
        public EnStatusTask Status { get; set; }
        public DateTime? Reminder { get; set; }

        // FK's
        public Guid IdList { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Name, "Name", "O campo 'Nome' não pode estar vazio")
                    .IsNotEmpty(Description, "Description", "O campo 'Descrição' não pode estar vazio")
                    .IsNotNull(Reminder, "Reminder", "O campo 'Lembrete' não pode ser nulo")
                    .IsNotEmpty(IdList, "IdList", "O campo 'IdList' não pode estar vazio")
                    .IsNotNull(Priority, "Priority", "O campo 'Prioridade' não pode ser nulo")
                    .IsNotNull(Status, "Status", "O campo 'Status' não pode ser nulo")
            );
        }
    }
}
