using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acades.Saga.Events;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.Options;
using Acades.Saga.States;

namespace Acades.Saga.Coordinators
{
    public interface ISagaCoordinator
    {
        Task<ISagaRunningState> GetSagaState(Guid id);
        Task<ISaga> Publish(ISagaEvent @event, TimeSpan? timeout = null);
        Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, TimeSpan? timeout = null);
        Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, SagaRunOptions runOptions, TimeSpan? timeout = null);
        Task ResumeAll();
        Task Resume(Guid id);
        Task WaitForIdle(Guid id, SagaWaitOptions waitOptions = null);
    }

    internal interface ISagaInternalCoordinator
    {
        Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, Guid? parentId, SagaRunOptions runOptions, TimeSpan? timeout = null);
    }
}
