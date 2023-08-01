using AcadesArchitecturePattern.Domain.Events;
using MediatR;

namespace AcadesArchitecturePattern.Application.Handlers.Events
{
    public class UserEventHandle : INotificationHandler<UserEvent>
    {
        public Task Handle(UserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
