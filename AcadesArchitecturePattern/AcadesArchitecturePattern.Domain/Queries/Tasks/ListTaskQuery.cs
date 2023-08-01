using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Queries.Tasks
{
    public class ListTaskQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        public void Validate() { }
    }
}
