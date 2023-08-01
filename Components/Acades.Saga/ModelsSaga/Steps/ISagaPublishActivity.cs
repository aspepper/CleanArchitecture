using System;
using Acades.Saga.Events;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Delegates;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga.ModelsSaga.Steps
{
    public interface ISagaPublishActivity<TSagaData, TExecuteEvent, TCompensateEvent> : ISagaStep
        where TSagaData : ISagaData
        where TExecuteEvent : ISagaEvent, new()
        where TCompensateEvent : ISagaEvent, new()
    {
        SendActionAsyncDelegate<TSagaData, TExecuteEvent> ActionDelegate { get; set; }
        SendActionAsyncDelegate<TSagaData, TCompensateEvent> CompensateDelegate { get; set; }
    }
}