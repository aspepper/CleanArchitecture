using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaEventNotRegisteredException : Exception
    {
        public SagaEventNotRegisteredException(Type eventType) :
            base($"Evento do tipo {eventType.Name} não está registrado")
        {
        }

        protected SagaEventNotRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}