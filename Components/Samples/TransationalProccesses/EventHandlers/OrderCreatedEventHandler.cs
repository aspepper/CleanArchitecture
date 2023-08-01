using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using TransationalProccesses.Events;

namespace TransationalProccesses.EventHandlers
{
    public class OrderCreatedEventHandler : ISagaEventHandler<OrderData, OrderCreatedEvent>
    {
        public OrderCreatedEventHandler()
        {
        }

        public Task Compensate(IExecutionContext<OrderData> context, OrderCreatedEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task Execute(IExecutionContext<OrderData> context, OrderCreatedEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
