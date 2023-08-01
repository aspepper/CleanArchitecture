using System;
using Acades.Saga.ModelsSaga.Steps;

namespace Acades.Saga.ModelsSaga.Actions.Interfaces
{
    public interface ISagaAction
    {
        SagaSteps ChildSteps { get; }
        Type Event { get; }
        string State { get; }
    }
}
