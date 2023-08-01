using System;

namespace Acades.Saga.Options
{
    public class SagaWaitOptions
    {
        public SagaWaitOptions()
        {
            Timeout = TimeSpan.FromSeconds(30);
        }

        public TimeSpan Timeout { get; set; }
    }
}