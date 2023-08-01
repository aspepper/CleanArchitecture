using System;

namespace Acades.Saga.Models.Interfaces
{
    public interface ISagaData
    {
        /// <summary>
        ///     ID de correlação do Saga
        /// </summary>
        public Guid ID { get; set; }
    }

    public class EmptySagaData : ISagaData
    {
        public Guid ID { get; set; }
    }
}
