using AcadesArchitecturePattern.Shared.Enums;
using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadesArchitecturePattern.Domain.Queries.ToDoLists
{
    public class SearchToDoListByIdQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
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
            public Guid IdUser { get; set; }
            public string Title { get; set; } = string.Empty;
            public EnColor Color { get; set; }
        }
    }
}
