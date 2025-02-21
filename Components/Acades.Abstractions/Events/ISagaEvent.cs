using System;

namespace Acades.Saga.Events;

public interface ISagaEvent
{
    /// <summary>
    ///     Saga's correlation ID
    /// </summary>
    Guid ID { get; set; }
}