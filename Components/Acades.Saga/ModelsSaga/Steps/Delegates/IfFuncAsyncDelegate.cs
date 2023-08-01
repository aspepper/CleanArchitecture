using System.Threading.Tasks;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps.Delegates
{
    public delegate Task<bool> IfFuncAsyncDelegate<TSagaData>(IExecutionContext<TSagaData> context)
        where TSagaData : ISagaData;

    public delegate bool IfFuncDelegate<TSagaData>(IExecutionContext<TSagaData> context)
        where TSagaData : ISagaData;
}
