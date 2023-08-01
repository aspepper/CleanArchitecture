using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Messages
{
    public class SagaAfterRetrivedMessage : IInternalMessage
    {
        public ISaga Saga;
        public SagaAfterRetrivedMessage(ISaga saga)
        {
            Saga = saga;
        }
    }
}
