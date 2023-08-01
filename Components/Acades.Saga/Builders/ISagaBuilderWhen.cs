using Acades.Saga.Events;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Builders
{
    public interface ISagaBuilderWhen<TSagaData> : ISagaBuilder<TSagaData>
        where TSagaData : ISagaData
    {
        ISagaBuilderThen<TSagaData, TEvent> When<TEvent>() where TEvent : ISagaEvent;

        ISagaBuilderThen<TSagaData, TEvent> When<TEvent, TEventHandler>() where TEvent : ISagaEvent
            where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;

        ISagaBuilderThen<TSagaData, TEvent> WhenAsync<TEvent, TEventHandler>() where TEvent : ISagaEvent
            where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;
    }
}
