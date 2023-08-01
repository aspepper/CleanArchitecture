using System;

namespace Acades.Saga.Handlers.ModelsHandlers
{
    public interface IHandlersModel
    {
        Type SagaStateType { get; }

        string Name { get; }

        bool ContainsEvent(Type type);

    }
}
