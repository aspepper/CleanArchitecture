namespace Acades.Saga.Observables.Interfaces
{
    internal interface IObservable
    {
        void Subscribe();

        void Unsubscribe();
    }
}
