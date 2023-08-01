using System.Threading.Tasks;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ExecutionContext
{
    public interface IExecutionContext<TSagaData> : IExecutionContext
        where TSagaData : ISagaData
    {
        TSagaData Data { get; }
    }

    public interface IExecutionContext
    {
        ISagaExecutionInfo ExecutionInfo { get; }

        ISagaExecutionState ExecutionState { get; }

        ISagaExecutionValues ExecutionValues { get; }

        IStepExecutionValues StepExecutionValues { get; }

        internal Task Stop();
    }
}
