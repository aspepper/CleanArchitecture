using AcadesArchitecturePattern.Domain.Events;
using MediatR;

namespace AcadesArchitecturePattern.Application.Handlers.Events
{
    public class ToDoListEventHandle : INotificationHandler<ToDoListEvent>
    {
        public Task Handle(ToDoListEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
