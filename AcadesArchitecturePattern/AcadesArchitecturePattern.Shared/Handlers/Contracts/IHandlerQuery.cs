using AcadesArchitecturePattern.Shared.Queries;

namespace AcadesArchitecturePattern.Shared.Handlers.Contracts
{
    public interface IHandlerQuery<T> where T : IQuery
    {
        IQueryResult Handler(T query);
    }
}
