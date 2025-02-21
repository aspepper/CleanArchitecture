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
    internal class SagaStepForThenInline<TSagaData>(
        string stepName,
        ThenAsyncActionDelegate<TSagaData> action,
        ThenAsyncActionDelegate<TSagaData>? compensation,
        bool async, ISagaStep parentStep) : ISagaStep
        where TSagaData : ISagaData
    {
        public SagaSteps ChildSteps { get; private set; } = new SagaSteps();
        public ISagaStep ParentStep { get; set; } = parentStep;
        public bool Async { get; set; } = async;
        public string StepName { get; set; } = stepName;

        private ThenAsyncActionDelegate<TSagaData> Action { get; } = action;
        private ThenAsyncActionDelegate<TSagaData>? Compensation { get; } = compensation;

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

            if (Action != null) { await Action(contextForAction); }
        }
    }
}
