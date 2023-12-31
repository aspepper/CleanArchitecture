﻿using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Acades.Saga.Exceptions
{
    [Serializable]
    public class SagaStepException : Exception
    {
        [JsonIgnore]
        public Exception OriginalException { get; set; }

        public Type ExceptionType { get; set; }

        public SagaStepException(Exception originalException) :
            base(originalException.Message)
        {
            this.ExceptionType = originalException.GetType();
            this.StackTrace = originalException.StackTrace;
            this.OriginalException = originalException;
        }

        protected SagaStepException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public string StackTrace { get; set; }
    }
}