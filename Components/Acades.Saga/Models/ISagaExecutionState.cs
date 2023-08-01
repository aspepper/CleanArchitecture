using System;
using Acades.Saga.Events;
using Acades.Saga.Models.History;
using Acades.Saga.ValueObjects;

namespace Acades.Saga.Models
{
    public interface ISagaExecutionState
    {
        Guid? ParentID { get; }
        AsyncExecution AsyncExecution { get; }
        Exception CurrentError { get; }
        ISagaEvent CurrentEvent { get; }
        string CurrentState { get; }
        string CurrentStep { get; }
        ExecutionID ExecutionID { get; }
        SagaHistory History { get; }
        bool IsCompensating { get; }
        bool IsResuming { get; }
        bool IsBreaked { get; }
    }
}