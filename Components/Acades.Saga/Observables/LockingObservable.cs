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
    internal class LockingObservable : IObservable
    {
        private readonly IServiceProvider serviceProvider;

        public LockingObservable(IServiceProvider serviceProvider)
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
            ISagaLocking sagaLocking = serviceProvider.GetRequiredService<ISagaLocking>();
            var id = msg?.Saga?.Data?.ID;
            if (id == null) { return; }
            if (!await sagaLocking.Acquire(id.Value)) { throw new SagaIsBusyException(id.Value); }
        }

        private async Task OnSagaProcessingEnd(ExecutionEndMessage msg)
        {
            ISagaLocking sagaLocking = serviceProvider.GetRequiredService<ISagaLocking>();
            var id = msg?.Saga?.Data?.ID;
            if (id == null)
                return;

            await sagaLocking.Banish(id.Value);
        }
    }
}
