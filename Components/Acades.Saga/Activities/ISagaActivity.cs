using Acades.Saga.ExecutionContext;
using Acades.Saga.Models.Interfaces;
using System.Threading.Tasks;

namespace Acades.Saga.Activities;

public interface ISagaActivity<TSagaData>
    where TSagaData : ISagaData
{
    Task Compensate(IExecutionContext<TSagaData> context);

    Task Execute(IExecutionContext<TSagaData> context);
}
