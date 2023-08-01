using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models.Interfaces;
using System;

namespace Acades.Saga.Messages
{
    public class ExecutionEndMessage : IInternalMessage
    {
        public ExecutionEndMessage(ISaga saga, Exception ex)
        {
            Saga = saga;
            Error = ex;
        }

        public ISaga Saga { get; set; }
        public Exception Error { get; set; }
    }
}
