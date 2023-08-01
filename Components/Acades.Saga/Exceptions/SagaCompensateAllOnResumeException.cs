using System;
using System.Runtime.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaCompensateAllOnResumeException : Exception
    {
        public SagaCompensateAllOnResumeException() :
            base("Compensando todas as etapas após exceções")
        {
        }
    }
}