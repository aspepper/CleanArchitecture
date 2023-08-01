using System;

namespace Acades.Saga.Events
{
    public class EmptyEvent : ISagaEvent
    {
        /// <summary>
        ///     ID de correlação do Saga
        /// </summary>
        public Guid ID { get => Guid.Empty; set { } }
    }
}