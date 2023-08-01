using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaStepNotRegisteredException : Exception
    {
        public SagaStepNotRegisteredException(Guid id, string state, string step) :
            base($"A etapa {step} não está registrada para o estado {state} (para a saga {id})")
        {
        }

        protected SagaStepNotRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}