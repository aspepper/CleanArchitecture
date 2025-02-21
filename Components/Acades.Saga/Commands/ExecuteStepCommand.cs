using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.Commands;

internal class ExecuteStepCommand 
{
    public ISagaModel Model;
    public ISaga Saga;
    public ISagaAction SagaAction;
    public ISagaStep SagaStep;
}
