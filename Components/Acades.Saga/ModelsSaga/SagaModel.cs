using System;
using Acades.Saga.ModelsSaga.Actions;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;

namespace Acades.Saga.ModelsSaga
{
    internal class SagaModel : ISagaModel
    {
        public SagaModel(Type SagaStateType)
        {
            this.Name = $"{SagaStateType.Name}Model";
            this.SagaStateType = SagaStateType;
            this.ResumePolicy = ESagaResumePolicy.DoCurrentStepCompensation;
            this.Actions = new SagaActions();
        }

        public ISagaActions Actions { get; }
        public string Name { get; set; }
        public ESagaResumePolicy ResumePolicy { get; set; }
        public Type SagaStateType { get; }
    }
}
