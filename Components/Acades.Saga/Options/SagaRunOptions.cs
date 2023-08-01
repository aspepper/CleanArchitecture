using System;

namespace Acades.Saga.Options
{
    public class SagaRunOptions
    {
        public SagaRunOptions()
        {
            CanBeResumed = true;
        }

        public Boolean CanBeResumed { get; set; }
    }
}