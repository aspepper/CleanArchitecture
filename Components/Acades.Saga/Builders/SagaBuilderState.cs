using Acades.Saga.ModelsSaga.Actions;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;
using Acades.Saga.Utils;
using System;

namespace Acades.Saga.Builders
{
    internal class SagaBuilderState(Type currentEvent, string currentState, ISagaModel model,
        IServiceProvider serviceProvider, UniqueNameGenerator uniqueNameGenerator, ISagaStep parentStep)
    {
        public ISagaStep ParentStep = parentStep;
        public Type CurrentEvent = currentEvent;
        public string CurrentState = currentState;
        public ISagaModel Model = model;
        public IServiceProvider ServiceProvider = serviceProvider;
        public UniqueNameGenerator UniqueNameGenerator = uniqueNameGenerator;
        public SagaAction? CurrentAction;
    }
}
