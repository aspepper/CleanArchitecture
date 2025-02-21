namespace Acades.Saga.Builders;

public interface ISagaSettingsBuilder
{
    ISagaSettingsBuilder OnResumeDoCurrentStepCompensation();
    ISagaSettingsBuilder OnResumeDoFullCompensation();
    ISagaSettingsBuilder OnResumeDoNothing();
}
