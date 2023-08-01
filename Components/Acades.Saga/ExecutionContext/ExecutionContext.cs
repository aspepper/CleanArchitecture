using System.Threading.Tasks;
using Acades.Saga.Exceptions;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ExecutionContext
{
    public class ExecutionContext<TSagaData> : IExecutionContext<TSagaData>
        where TSagaData : ISagaData
    {
        public ExecutionContext(TSagaData data, SagaExecutionInfo info, SagaExecutionState state, SagaExecutionValues sagaExecutionValues, StepExecutionValues stepExecutionValues)
        {
            Data = data;
            ExecutionInfo = info;
            ExecutionState = state;
            ExecutionValues = sagaExecutionValues;
            StepExecutionValues = stepExecutionValues;
        }

        public TSagaData Data { get; set; }

        public ISagaExecutionInfo ExecutionInfo { get; set; }

        public ISagaExecutionState ExecutionState { get; set; }

        public ISagaExecutionValues ExecutionValues { get; set; }

        public IStepExecutionValues StepExecutionValues { get; set; }

        Task IExecutionContext.Stop()
        {
            throw new SagaStopException();
        }
    }
}