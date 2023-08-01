using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Queries.Users
{
    public class SearchUserByUserNameQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        public string UserName { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(UserName, "UserName", "O campo 'UserName' não pode estar vazio")
                );
        }

        public class SearchUserByUserNameResult
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }

            // Compositions
            public IReadOnlyCollection<ToDoList> ToDoLists { get; private set; }
            private List<ToDoList> _lists { get; set; }
        }
    }
}
