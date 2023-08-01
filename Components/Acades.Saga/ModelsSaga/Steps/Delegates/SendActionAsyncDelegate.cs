using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps.Delegates
{
    public delegate Task SendActionAsyncDelegate<TSagaData, TEvent>(IExecutionContext<TSagaData> context, TEvent @event)
        where TSagaData : ISagaData
        where TEvent : ISagaEvent;

    public delegate void SendActionDelegate<TSagaData, TEvent>(IExecutionContext<TSagaData> context, TEvent @event)
        where TSagaData : ISagaData
        where TEvent : ISagaEvent;
}
