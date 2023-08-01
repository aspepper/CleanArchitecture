using AcadesArchitecturePattern.Shared.Commands;
using Flunt.Notifications;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Commands.ProccessWithTransaction
{
    public class RunAllProcesses : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {

        public RunAllProcesses() { }

        public Guid IdList { get; set; }

        public void Validate()
        { }
    }

}
