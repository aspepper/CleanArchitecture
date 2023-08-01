using System;
using Acades.Saga.ModelsSaga.Interfaces;

namespace Acades.Saga.Registrator.Interfaces
{
    public interface ISagaRegistrator
    {
        internal ISagaModel FindModelByName(string name);
        internal ISagaModel FindModelForEventType(Type eventType);
        void Register(ISagaModel model);
    }
}
