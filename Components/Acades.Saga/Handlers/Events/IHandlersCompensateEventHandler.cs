using Acades.Saga.Events;
using Acades.Saga.Handlers.ExecutionContext;
using System.Threading.Tasks;

namespace Acades.Saga.Handlers.Events
{
    public interface IHandlersCompensateEventHandler<TEvent> : ISagaEventHandler
        where TEvent : IHandlersEvent
    {
        Task Compensate(IHandlersExecutionContext context, TEvent @event);

        Task Execute(IHandlersExecutionContext context, TEvent @event);
    }
}