using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaIsBusyException : Exception
    {
        public SagaIsBusyException(Guid id) :
            base($"Saga {id} está ocupado")
        {
            Id = id;
        }

        protected SagaIsBusyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public Guid Id { get; }
    }
}