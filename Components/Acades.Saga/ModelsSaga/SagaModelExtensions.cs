using System;
using System.Linq;
using Acades.Saga.ModelsSaga.Actions;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.ModelsSaga
{
    internal static class SagaModelExtensions
    {
        public static SagaActions FindActionsByState(
            this ISagaModel model, string state)
        {
            return new SagaActions(model.Actions.Where(s => s.State == state));
        }
        public static ISagaAction FindActionForStateAndEvent(
            this ISagaModel model, string state, Type eventType)
            
        {
            ISagaAction action = model.FindActionByStateAndEventType(state, eventType) ?? throw new Exception($"Não foi possível encontrar ação para o estado {state} e evento do tipo {eventType?.Name}");
            return action;
        }

        public static ISagaAction FindActionForStep(
            this ISagaModel model, ISagaStep sagaStep)
            
        {
            return model.Actions.
                FindActionByStep(sagaStep?.StepName);
        }

        public static ISagaAction FindActionByStateAndEventType(
            this ISagaModel model, string stateName, Type eventType)

        {
            return model.Actions.FindActionByStateAndEventType(stateName, eventType);
        }
    }
}
