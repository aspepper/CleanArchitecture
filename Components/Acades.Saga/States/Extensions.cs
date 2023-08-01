using Acades.Saga.States.Interfaces;
using Acades.Saga.Utils;

namespace Acades.Saga.States
{
    internal static class Extensions
    {
        internal static string GetStateName<TState>(this TState state)
            where TState : ISagaState
        {
            if (typeof(TState).Is<IStateWithCustomName>()) { return ((IStateWithCustomName)state).Name; }
            return typeof(TState).Name;
        }
    }
}
