using Acades.Saga.ExecutionContext;
using Acades.Saga.Handlers.Events;
using System.Threading.Tasks;

namespace Acades.Saga.Handlers.Delegates
{
    public delegate Task HandlersSendActionDelegate<TEvent>(IExecutionContext<HandlersData> context, TEvent @event)
        where TEvent : IHandlersEvent;
}