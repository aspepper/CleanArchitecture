using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Acades.Saga.Exceptions;
using Acades.Saga.Locking;
using Acades.Saga.MessageBus;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Messages;
using Acades.Saga.Observables.Interfaces;

namespace Acades.Saga.Observables
{
    internal class CallbacksObservable : IObservable
    {
        private readonly IServiceProvider serviceProvider;

        public CallbacksObservable(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Subscribe()
        {
            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            messageBus.Subscribe<ExecutionStartMessage>(this, OnSagaProcessingStart);
            messageBus.Subscribe<ExecutionEndMessage>(this, OnSagaProcessingEnd);
        }

        public void Unsubscribe()
        {
            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            messageBus.Unsubscribe<ExecutionStartMessage>(this);
            messageBus.Unsubscribe<ExecutionEndMessage>(this);
        }


        private async Task OnSagaProcessingStart(ExecutionStartMessage msg)
        {
            await Callbacks.ExecuteBeforeRequestCallbacks(serviceProvider, msg.Saga);
        }

        private async Task OnSagaProcessingEnd(ExecutionEndMessage msg)
        {
            await Callbacks.ExecuteAfterRequestCallbacks(serviceProvider, msg.Saga, msg.Error);
        }
    }
}
