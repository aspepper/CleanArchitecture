using Acades.Saga.Models;
using System.Threading.Tasks;

namespace Acades.Saga.Handlers.ExecutionContext
{
    public interface IHandlersExecutionContext
    {
        SagaExecutionInfo Info { get; }

        SagaExecutionState State { get; }

        internal Task Stop();
    }
}