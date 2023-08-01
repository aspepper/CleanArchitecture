namespace Acades.Saga.States.Interfaces
{
    public interface IStateWithCustomName : ISagaState
    {
        string Name { get; }
    }
}
