using Acades.Saga.Builders;
using Acades.Saga.ModelsSaga.Interfaces;
using TransationalProccesses.Activities;
using TransationalProccesses.EventHandlers;
using TransationalProccesses.Events;
using TransationalProccesses.States;

namespace TransationalProccesses
{
    internal class OrderSagaBuilder : ISagaModelBuilder<OrderData>
    {
        ISagaBuilder<OrderData> builder;

        public OrderSagaBuilder(ISagaBuilder<OrderData> builder)
        {
            this.builder = builder;
        }

        public ISagaModel Build()
        {
            builder.
                Name(nameof(OrderSagaBuilder));

            builder.
                Start<OrderCreatedEvent, OrderCreatedEventHandler>("OrderCreatedEventStep0").
                    Then("OrderCreatedEventStep1", ctx => Task.CompletedTask).
                    TransitionTo<StateCreated>();

            builder.
                During<StateCreated>().
                When<OrderCompletedEvent>().
                    Then("OrderCompletedEventStep1", ctx => Task.CompletedTask).
                    Then<CarRentalEvent>("email").
                    Then<HotelReservationEvent>("SendMessageToTheManagerEventStep").
                    Then<FlightReservationEvent>("OrderCourierEventStep").
                    TransitionTo<StateCompleted>();

            builder.
                During<StateCompleted>().
                When<OrderSendEvent>().
                    Then("OrderSendEventStep1", ctx => Task.CompletedTask).
                    TransitionTo<StateOrderSend>();

            builder.
                During<StateOrderSend>().
                When<DeliveredEvent>().
                    Then("DeliveredEventStep1", ctx => Task.CompletedTask).
                    Finish();

            builder.
                During<StateCreated>().
                When<ToAlternative1Event>().
                    TransitionTo<StateAlternative1>().
                When<ToAlternative2Event>().
                    TransitionTo<StateAlternative2>();

            return builder.
                Build();
        }
    }
}
