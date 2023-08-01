using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    internal class SagaStopException : Exception
    {
        public SagaStopException()
        {
        }

        protected SagaStopException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}