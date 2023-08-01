using System;

namespace Acades.Saga.Models
{
    public class SagaExecutionInfo : ISagaExecutionInfo
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string ModelName { get; set; }
    }
}