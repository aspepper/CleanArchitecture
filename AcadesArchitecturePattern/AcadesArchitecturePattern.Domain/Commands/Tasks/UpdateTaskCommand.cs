using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Enums;
using Flunt.Notifications;
using MediatR;
using System.Text.Json.Serialization;

namespace AcadesArchitecturePattern.Domain.Commands.Tasks
{
    public class UpdateTaskCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        public UpdateTaskCommand() { }

        public UpdateTaskCommand(string name, string description, EnTaskPriorityLevel priority, EnStatusTask status, DateTime? reminder)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
            Reminder = reminder;
        }

        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EnTaskPriorityLevel? Priority { get; set; }
        public EnStatusTask? Status { get; set; }
        public DateTime? Reminder { get; set; }

        public void Validate()
        {
            // Empty
        }
    }
}
