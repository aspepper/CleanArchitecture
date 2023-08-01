using Acades.Saga.Handlers.Events;
using Acades.Saga.Handlers.ModelsHandlers;

namespace Acades.Saga.Handlers.Builders
{
    public interface IHandlersBuilder
    {
        IHandlersModel Build();

        IHandlersBuilderHandle<TEvent> When<TEvent>() where TEvent : IHandlersEvent;

        IHandlersBuilderHandle<TEvent> When<TEvent, TEventHandler>() where TEvent : IHandlersEvent
            where TEventHandler : IHandlersCompensateEventHandler<TEvent>;

        IHandlersBuilderHandle<TEvent> WhenAsync<TEvent, TEventHandler>() where TEvent : IHandlersEvent
            where TEventHandler : IHandlersCompensateEventHandler<TEvent>;
    }
}
