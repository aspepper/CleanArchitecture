using Acades.Saga.ModelsSaga.Interfaces;

namespace Acades.Saga.Builders;

public class SagaSettingsBuilder(ISagaModel model) : ISagaSettingsBuilder
{
    private readonly ISagaModel model = model;

    public ISagaSettingsBuilder OnResumeDoCurrentStepCompensation()
    {
        model.ResumePolicy = ESagaResumePolicy.DoCurrentStepCompensation;
        return this;
    }

    public ISagaSettingsBuilder OnResumeDoFullCompensation()
    {
        model.ResumePolicy = ESagaResumePolicy.DoFullCompensation;
        return this;
    }

    public ISagaSettingsBuilder OnResumeDoNothing()
    {
        model.ResumePolicy = ESagaResumePolicy.DoNothing;
        return this;
    }
}
