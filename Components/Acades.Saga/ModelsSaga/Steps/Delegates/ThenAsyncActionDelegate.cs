using System.Threading.Tasks;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps.Delegates
{
    public delegate Task ThenAsyncActionDelegate<TSagaData>(IExecutionContext<TSagaData> context)
        where TSagaData : ISagaData;

    public delegate void ThenActionDelegate<TSagaData>(IExecutionContext<TSagaData> context)
        where TSagaData : ISagaData;
}
