using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models;
using Acades.Saga.Models.History;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;
using System;
using System.Threading.Tasks;

namespace Acades.Saga.ModelsSaga.Steps
{
    internal class SagaStepForBreak<TSagaData> : ISagaStep
        where TSagaData : ISagaData
    {
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }
        public SagaStepForBreak(
            string StepName, bool async, ISagaStep parentStep)
        {
            this.StepName = StepName;
            Async = async;
            ChildSteps = new SagaSteps();
            ParentStep = parentStep;
        }

        public async Task Compensate(
            IServiceProvider serviceProvider,
            IExecutionContext context,
            ISagaEvent @event,
            IStepData stepData)
        {
            

        }

        public async Task Execute(
            IServiceProvider serviceProvider,
            IExecutionContext context,
            ISagaEvent @event,
            IStepData stepData)
        {
            (context.ExecutionState as SagaExecutionState).IsBreaked = true;
        }
    }
}
