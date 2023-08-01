using AcadesArchitecturePattern.Domain.Events;
using MediatR;

namespace AcadesArchitecturePattern.Application.Handlers.Events
{
    public class TaskEventHandle : INotificationHandler<TaskEvent>
    {
        public Task Handle(TaskEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}