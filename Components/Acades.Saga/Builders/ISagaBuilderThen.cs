﻿using Acades.Saga.Activities;
using Acades.Saga.Conditions;
using Acades.Saga.Events;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Delegates;
using Acades.Saga.States.Interfaces;
using System;

namespace Acades.Saga.Builders;

public interface ISagaBuilderThen<TSagaData, TEvent> : ISagaBuilderWhen<TSagaData>
    where TSagaData : ISagaData
    where TEvent : ISagaEvent
{
    ISagaBuilderThen<TSagaData, TEvent> HandleBy<TEventHandler>()
        where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;
    ISagaBuilderThen<TSagaData, TEvent> HandleByAsync<TEventHandler>()
        where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;

    ISagaBuilderThen<TSagaData, TEvent> HandleBy<TEventHandler>(string stepName)
        where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;
    ISagaBuilderThen<TSagaData, TEvent> HandleByAsync<TEventHandler>(string stepName)
        where TEventHandler : ISagaEventHandler<TSagaData, TEvent>;

    ISagaBuilderThen<TSagaData, TEvent> After(TimeSpan time);

    ISagaModel Build();

    ISagaBuilder<TSagaData> Finish();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend>()
        where TEventToSend : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend>(
        SendActionAsyncDelegate<TSagaData, TEventToSend> action)
        where TEventToSend : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend>(
        SendActionDelegate<TSagaData, TEventToSend> action)
        where TEventToSend : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend, TCompensateEvent>()
        where TEventToSend : ISagaEvent, new()
        where TCompensateEvent : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend, TCompensateEvent>(
        SendActionAsyncDelegate<TSagaData, TEventToSend> action,
        SendActionAsyncDelegate<TSagaData, TCompensateEvent> compensation)
        where TEventToSend : ISagaEvent, new()
        where TCompensateEvent : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Publish<TEventToSend, TCompensateEvent>(
        SendActionDelegate<TSagaData, TEventToSend> action,
        SendActionDelegate<TSagaData, TCompensateEvent> compensation)
        where TEventToSend : ISagaEvent, new()
        where TCompensateEvent : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent> Do(Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> While<TSagaCondition>(Action<ISagaBuilderThen<TSagaData, TEvent>> builder)
        where TSagaCondition : ISagaCondition<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> While(IfFuncAsyncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> While(IfFuncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> If<TSagaCondition>(Action<ISagaBuilderThen<TSagaData, TEvent>> builder)
        where TSagaCondition : ISagaCondition<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> If(IfFuncAsyncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> ElseIf(IfFuncAsyncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> If(IfFuncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> ElseIf(IfFuncDelegate<TSagaData> action, Action<ISagaBuilderThen<TSagaData, TEvent>> builder);

    ISagaBuilderThen<TSagaData, TEvent> Else(Action<ISagaBuilderThen<TSagaData, TEvent>> builder);
    ISagaBuilderThen<TSagaData, TEvent> Then(ThenAsyncActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> Then(string stepName, ThenAsyncActionDelegate<TSagaData> action);

    ISagaBuilderThen<TSagaData, TEvent> Then(ThenAsyncActionDelegate<TSagaData> action,
        ThenAsyncActionDelegate<TSagaData> compensation);

    ISagaBuilderThen<TSagaData, TEvent> Then(string stepName, ThenAsyncActionDelegate<TSagaData> action,
        ThenAsyncActionDelegate<TSagaData> compensation);

    ISagaBuilderThen<TSagaData, TEvent> Then(ThenActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> Then(string stepName, ThenActionDelegate<TSagaData> action);

    ISagaBuilderThen<TSagaData, TEvent> Then(ThenActionDelegate<TSagaData> action,
        ThenActionDelegate<TSagaData> compensation);
    ISagaBuilderThen<TSagaData, TEvent> Then(string stepName, ThenActionDelegate<TSagaData> action,
        ThenActionDelegate<TSagaData> compensation);

    ISagaBuilderThen<TSagaData, TEvent> Then<TSagaActivity>() where TSagaActivity : ISagaActivity<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> Then<TSagaActivity>(string stepName) where TSagaActivity : ISagaActivity<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(ThenAsyncActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(string stepName, ThenAsyncActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(ThenAsyncActionDelegate<TSagaData> action,
        ThenAsyncActionDelegate<TSagaData> compensation);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(string stepName, ThenAsyncActionDelegate<TSagaData> action,
        ThenAsyncActionDelegate<TSagaData> compensation);


    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(ThenActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(string stepName, ThenActionDelegate<TSagaData> action);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(ThenActionDelegate<TSagaData> action,
        ThenActionDelegate<TSagaData> compensation);
    ISagaBuilderThen<TSagaData, TEvent> ThenAsync(string stepName, ThenActionDelegate<TSagaData> action,
        ThenActionDelegate<TSagaData> compensation);

    ISagaBuilderThen<TSagaData, TEvent> ThenAsync<TSagaActivity>() where TSagaActivity : ISagaActivity<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> ThenAsync<TSagaActivity>(string stepName)
        where TSagaActivity : ISagaActivity<TSagaData>;

    ISagaBuilderThen<TSagaData, TEvent> TransitionTo<TState>() where TState : ISagaState;

    ISagaBuilderThen<TSagaData, TEvent2> MoveTo<TState, TEvent2>()
        where TState : ISagaState
        where TEvent2 : ISagaEvent, new();

    ISagaBuilderThen<TSagaData, TEvent2> MoveTo<TState, TEvent2>(
        Func<TSagaData, TEvent2> createEventDelegate = null)
        where TState : ISagaState
        where TEvent2 : ISagaEvent;

    ISagaBuilderThen<TSagaData, TEvent> Abort();

}
