using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Acades.Saga.Coordinators;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.Models.History;
using Acades.Saga.ModelsSaga.Steps.Delegates;
using Acades.Saga.ModelsSaga.Steps.Interfaces;
using Acades.Saga.Options;

namespace Acades.Saga.ModelsSaga.Steps
{
    public class SagaStepForPublishActivity<TSagaData, TExecuteEvent, TCompensateEvent> : ISagaStep, ISagaPublishActivity<TSagaData, TExecuteEvent, TCompensateEvent>
        where TSagaData : ISagaData
        where TExecuteEvent : ISagaEvent, new()
        where TCompensateEvent : ISagaEvent, new()
    {
        public SendActionAsyncDelegate<TSagaData, TExecuteEvent> ActionDelegate { get; set; }
        public SendActionAsyncDelegate<TSagaData, TCompensateEvent> CompensateDelegate { get; set; }
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }

        public SagaStepForPublishActivity()
        {
            ChildSteps = new SagaSteps();
        }

        public async Task Compensate(
            IServiceProvider serviceProvider,
            IExecutionContext context,
            ISagaEvent @event,
            IStepData stepData)
        {
            if (typeof(TCompensateEvent) == typeof(EmptyEvent))
                return;

            ISagaCoordinator sagaCoordinator = serviceProvider.
                GetRequiredService<ISagaCoordinator>();

            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            TCompensateEvent compensationEvent = new();
            if (CompensateDelegate != null)
            { await CompensateDelegate(contextForAction, compensationEvent); }

            await sagaCoordinator.
                Publish(compensationEvent, contextForAction.ExecutionValues);
        }

        public async Task Execute(
            IServiceProvider serviceProvider,
            IExecutionContext context,
            ISagaEvent @event,
            IStepData stepData)
        {
            if (typeof(TExecuteEvent) == typeof(EmptyEvent)) { return; }

            ISagaInternalCoordinator sagaCoordinator = serviceProvider.
                GetRequiredService<ISagaCoordinator>() as ISagaInternalCoordinator;

            IExecutionContext<TSagaData> contextForAction =
                (IExecutionContext<TSagaData>)context;

            TExecuteEvent executionEvent = new();
            if (ActionDelegate != null)
            { await ActionDelegate(contextForAction, executionEvent); }

            _ = await sagaCoordinator.
                Publish(executionEvent, contextForAction.ExecutionValues, contextForAction.Data.ID, new SagaRunOptions());
        }
    }
}
