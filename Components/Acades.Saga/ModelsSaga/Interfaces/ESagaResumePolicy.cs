namespace Acades.Saga.ModelsSaga.Interfaces
{
    public enum ESagaResumePolicy
    {
        DoCurrentStepCompensation = 0,
        DoFullCompensation = 1,
        DoNothing = 2
    }
}
