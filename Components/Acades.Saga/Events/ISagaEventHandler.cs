using System.Threading.Tasks;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Events
{
    public interface ISagaEventHandler<TSagaData, TEvent> : ISagaEventHandler
        where TSagaData : ISagaData
        where TEvent : ISagaEvent
    {
        Task Compensate(IExecutionContext<TSagaData> context, TEvent @event);

        Task Execute(IExecutionContext<TSagaData> context, TEvent @event);
    }
}
