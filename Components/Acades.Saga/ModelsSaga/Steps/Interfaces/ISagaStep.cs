using System;
using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.History;

namespace Acades.Saga.ModelsSaga.Steps.Interfaces
{
    public interface ISagaStep
    {
        SagaSteps ChildSteps { get; }
        ISagaStep ParentStep { get; set; }
        bool Async { get; set; }
        string StepName { get; set; }
        Task Compensate(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData);
        Task Execute(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData);
    }
}
