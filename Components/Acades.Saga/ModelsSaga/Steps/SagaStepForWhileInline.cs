using System;
using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.Models.History;
using Acades.Saga.ModelsSaga.Steps.Delegates;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps
{
    internal class SagaStepForWhileInline<TSagaData> : ISagaStep, ISagaStepForWhile
        where TSagaData : ISagaData
    {
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }

        public SagaStepForWhileInline(
            string StepName,
            IfFuncAsyncDelegate<TSagaData> action,
            ThenAsyncActionDelegate<TSagaData> compensation,
            ISagaStep parentStep)
        {
            this.StepName = StepName;
            this.Action = action;
            this.Compensation = compensation;
            this.Async = false;
            this.ChildSteps = new SagaSteps();
            this.ParentStep = parentStep;
        }

        private IfFuncAsyncDelegate<TSagaData> Action { get; }
        private ThenAsyncActionDelegate<TSagaData> Compensation { get; }

        public void SetChildSteps(SagaSteps steps)
        {
            this.ChildSteps = steps;
        }

        public async Task Compensate(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            if (Compensation != null) { await Compensation(contextForAction); }
        }

        public async Task Execute(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            if (Action != null)
            {
                bool result = await Action(contextForAction);
                stepData.ExecutionData.ConditionResult = result;
            }
            else
            {
                stepData.ExecutionData.ConditionResult = false;
            }
        }
    }
}
