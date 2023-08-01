using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaInvalidEventForStateException : Exception
    {
        public SagaInvalidEventForStateException(Guid id, string currentState, Type eventType) :
            base($"Saga {id} no estado {currentState} não tem suporte para eventos do tipo {(eventType != null ? eventType.Name : "null")}!")
        { }

        protected SagaInvalidEventForStateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}