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
    internal class SagaStepForThenInline<TSagaData> : ISagaStep
        where TSagaData : ISagaData
    {
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }

        public SagaStepForThenInline(
            string stepName,
            ThenAsyncActionDelegate<TSagaData> action,
            ThenAsyncActionDelegate<TSagaData> compensation,
            bool async, ISagaStep parentStep)
        {
            StepName = stepName;
            this.Action = action;
            this.Compensation = compensation;
            Async = async;
            ChildSteps = new SagaSteps();
            ParentStep = parentStep;
        }

        private ThenAsyncActionDelegate<TSagaData> Action { get; }
        private ThenAsyncActionDelegate<TSagaData> Compensation { get; }

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
