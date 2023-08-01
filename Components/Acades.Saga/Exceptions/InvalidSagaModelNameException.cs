using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class InvalidSagaModelNameException : Exception
    {
        public InvalidSagaModelNameException() :
            base("O nome do modelo Saga não pode ficar vazio!")
        {
        }

        protected InvalidSagaModelNameException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}