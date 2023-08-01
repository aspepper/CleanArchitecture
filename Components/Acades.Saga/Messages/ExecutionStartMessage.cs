using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;

namespace Acades.Saga.Messages
{
    public class ExecutionStartMessage : IInternalMessage
    {
        public ISagaModel Model;
        public ISaga Saga;

        public ExecutionStartMessage(ISaga saga, ISagaModel model)
        {
            Saga = saga;
            Model = model;
        }
    }
}
