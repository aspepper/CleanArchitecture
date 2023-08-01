﻿using System;
using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.History;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps
{
    internal class SagaContainerStep : ISagaStep
    {
        public SagaSteps ChildSteps { get; private set; }
        public ISagaStep ParentStep { get; set; }
        public bool Async { get; set; }
        public string StepName { get; set; }

        public SagaContainerStep(
            string StepName, ISagaStep parentStep)
        {
            this.StepName = StepName;
            Async = false;
            ChildSteps = new SagaSteps();
            ParentStep = parentStep;
        }

        public void SetChildSteps(SagaSteps steps)
        {
            this.ChildSteps = steps;
        }

        public Task Compensate(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            return Task.CompletedTask;
        }

        public Task Execute(IServiceProvider serviceProvider, IExecutionContext context, ISagaEvent @event, IStepData stepData)
        {
            return Task.CompletedTask;
        }
    }
}
