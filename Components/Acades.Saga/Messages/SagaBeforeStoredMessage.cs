using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Messages
{
    public class SagaBeforeStoredMessage : IInternalMessage
    {
        public ISaga Saga;
        public SagaBeforeStoredMessage(ISaga saga)
        {
            Saga = saga;
        }
    }
}
