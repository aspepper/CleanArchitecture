using System;
using System.Threading.Tasks;
using Acades.Saga.ExecutionContext;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Conditions
{
    public interface ISagaCondition<TSagaData> : ISagaCondition
        where TSagaData : ISagaData
    {
        Task Compensate(IExecutionContext<TSagaData> context);

        Task<bool> Execute(IExecutionContext<TSagaData> context);
    }

    public interface ISagaCondition
    {
    }
}
