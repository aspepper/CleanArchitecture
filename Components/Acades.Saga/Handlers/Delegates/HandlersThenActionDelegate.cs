using System.Threading.Tasks;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Handlers.Events;

namespace Acades.Saga.Handlers.Delegates
{
    public delegate Task HandlersThenActionDelegate(IExecutionContext<HandlersData> context);
}