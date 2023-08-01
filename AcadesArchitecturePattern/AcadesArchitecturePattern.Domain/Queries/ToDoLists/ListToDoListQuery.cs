using AcadesArchitecturePattern.Shared.Queries;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Queries.ToDoLists
{
    public class ListToDoListQuery : IQuery, IRequest<GenericQueryResult>
    {
        public void Validate() { }
    }
}
