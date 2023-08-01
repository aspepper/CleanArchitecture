using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Handlers.Events;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Activities
{
    public interface ISagaActivity<TSagaData>
        where TSagaData : ISagaData
    {
        Task Compensate(IExecutionContext<TSagaData> context);

        Task Execute(IExecutionContext<TSagaData> context);
    }

}
