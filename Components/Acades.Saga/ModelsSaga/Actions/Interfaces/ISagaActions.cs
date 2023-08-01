using System;
using System.Collections.Generic;

namespace Acades.Saga.ModelsSaga.Actions.Interfaces
{
    public interface ISagaActions : IEnumerable<ISagaAction>
    {
        void Add(ISagaAction action);
        bool IsStartEvent(Type eventType);
        bool IsEventSupported(Type eventType);
    }
}
