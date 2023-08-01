using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{

    [Serializable]
    public class CountNotExecuteDeletedSagaException : Exception
    {
        public CountNotExecuteDeletedSagaException(Guid id) :
            base($"Contagem não executada saga excluída {id}!")
        {
        }

        protected CountNotExecuteDeletedSagaException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class SagaInstanceNotFoundException : Exception
    {
        public SagaInstanceNotFoundException(Type sagaStateType, Guid id) :
            base($"Saga com id {id} não encontrado (tipo de estado {sagaStateType.Name})!")
        {
        }

        public SagaInstanceNotFoundException(Type sagaStateType) :
            base($"Saga não encontrada (tipo de estado {sagaStateType.Name})!")
        {
        }

        public SagaInstanceNotFoundException(Guid id) :
            base($"Saga com id {id} não encontrado!")
        {
        }


        protected SagaInstanceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}