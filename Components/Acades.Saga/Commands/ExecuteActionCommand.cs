using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.ValueObjects;

namespace Acades.Saga.Commands;

internal class ExecuteActionCommand
{
    public AsyncExecution Async;
    public ISagaModel Model;
    public ISaga Saga { get; internal set; }
}
