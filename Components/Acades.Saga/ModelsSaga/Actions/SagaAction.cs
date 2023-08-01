using System;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Steps;

namespace Acades.Saga.ModelsSaga.Actions
{

    public class SagaAction : ISagaAction
    {
        public SagaAction()
        {
            ChildSteps = new SagaSteps();
        }

        public Type Event { get; set; }
        public string State { get; set; }
        public SagaSteps ChildSteps { get; set; }

    }
}
