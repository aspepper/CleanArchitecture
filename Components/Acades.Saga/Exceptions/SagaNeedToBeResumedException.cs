using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaNeedToBeResumedException : Exception
    {
        public SagaNeedToBeResumedException(Guid id) :
            base($"Saga {id} precisa ser retomada")
        {
        }

        protected SagaNeedToBeResumedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}