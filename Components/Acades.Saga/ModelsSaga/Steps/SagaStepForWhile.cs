using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Acades.Saga.Conditions;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.Models.History;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps
{
    internal class SagaStepForWhile<TSagaData, TSagaCondition> : ISagaStep, ISagaStepForWhile
        where TSagaData : ISagaData
        where TSagaCondition : ISagaCondition<TSagaData>
    {
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }

        public SagaStepForWhile(string StepName, ISagaStep parentStep)
        {
            this.StepName = StepName;
            this.Async = false;
            this.ChildSteps = new SagaSteps();
            this.ParentStep = parentStep;
        }

        public void SetChildSteps(SagaSteps steps)
        {
            this.ChildSteps = steps;
        }

        public async Task Compensate(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            TSagaCondition activity = (TSagaCondition)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TSagaCondition));

            if (activity != null) { await activity.Compensate(contextForAction); }
        }

        public async Task Execute(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            TSagaCondition activity = (TSagaCondition)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TSagaCondition));

            if (activity != null)
            {
                bool result = await activity.Execute(contextForAction);
                stepData.ExecutionData.ConditionResult = result;
            }
            else
            {
                stepData.ExecutionData.ConditionResult = false;
            }
        }
    }
}
