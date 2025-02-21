using AcadesArchitecturePattern.Shared.Enums;
using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Queries.Tasks
{
    public class SearchTaskByIdQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Id, "id", "O campo 'id' não pode estar vazio")
                );
        }

        public class SearchByIdResult
        {
            public Guid Id { get; set; }
            public Guid IdList { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public EnTaskPriorityLevel Priority { get; set; }
            public EnStatusTask Status { get; set; }
            public DateTime? Reminder { get; set; }
            public DateTime InsertDate { get; set; }
            public DateTime? ModifyDate { get; set; }
        }
    }
}
