using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadesArchitecturePattern.Domain.Queries.Users
{
    public class ListUserQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        public void Validate() { }
    }
}
