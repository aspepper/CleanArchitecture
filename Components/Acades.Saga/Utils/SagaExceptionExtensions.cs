using System;
using System.Text.Json;
using Acades.Saga.Exceptions;

namespace Acades.Saga.Utils
{
    internal static class SagaExceptionExtensions
    {
        internal static Exception ToSagaStepException(this Exception exception)
        {
            if (exception == null) { return null; }
            if (IsSerializable(exception)) { return exception; }
            return new SagaStepException(exception);
        }

        static bool IsSerializable(Exception ex)
        {
            Type exceptionType = ex.GetType();
            return exceptionType.IsSerializable;
        }
    }
}