using Acades.Saga.ExecutionContext;
using Acades.Saga.Handlers.Events;
using System.Threading.Tasks;

namespace Acades.Saga.Handlers.Activities
{
    public interface IHandlersActivity
    {
        Task Compensate(IExecutionContext<HandlersData> context);

        Task Execute(IExecutionContext<HandlersData> context);
    }
}