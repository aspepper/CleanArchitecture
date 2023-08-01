using Acades.Saga.Commands;
using Acades.Saga.Commands.Handlers;
using Acades.Saga.Events;
using Acades.Saga.Exceptions;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Messages;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.Observables.Registrator;
using Acades.Saga.Options;
using Acades.Saga.Persistance;
using Acades.Saga.Providers.Interfaces;
using Acades.Saga.Registrator.Interfaces;
using Acades.Saga.States;
using Acades.Saga.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Acades.Saga.Coordinators
{
    public class SagaCoordinator : ISagaCoordinator, ISagaInternalCoordinator
    {
        private static int isResuming;
        private readonly ILogger logger;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMessageBus messageBus;
        private readonly ISagaPersistance sagaPersistance;
        private readonly ISagaRegistrator sagaRegistrator;
        private readonly IServiceProvider serviceProvider;

        public SagaCoordinator(ISagaRegistrator sagaRegistrator, ISagaPersistance sagaPersistance,
            IMessageBus messageBus, IDateTimeProvider dateTimeProvider,
            IServiceProvider serviceProvider, ILogger logger)
        {
            this.sagaRegistrator = sagaRegistrator;
            this.sagaPersistance = sagaPersistance;
            this.messageBus = messageBus;
            this.dateTimeProvider = dateTimeProvider;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public async Task<ISagaRunningState> GetSagaState(Guid id)
        {
            ISaga saga = await sagaPersistance.Get(id);
            if (saga == null) { return null; }

            return new SagaRunningState
            {
                IsRunning = !saga.IsIdle(),
                IsResuming = saga.ExecutionState.IsResuming,
                IsCompensating = saga.ExecutionState.IsCompensating
            };
        }

        public async Task ResumeAll()
        {
            isResuming++;
            try
            {
                IList<Guid> ids = await sagaPersistance.GetUnfinished();

                List<string> invalidModels = new();
                foreach (Guid id in ids)
                {
                    ISaga saga = await sagaPersistance.Get(id);
                    ISagaModel model = sagaRegistrator.FindModelByName(saga.ExecutionInfo.ModelName);

                    if (model == null) { invalidModels.Add(saga.ExecutionInfo.ModelName); }
                }

                if (invalidModels.Count > 0) { throw new Exception($"Modelos Saga {string.Join(", ", invalidModels.Distinct().ToArray())} não encontrados"); }

                foreach (Guid id in ids)
                {
                    ISaga saga = await sagaPersistance.Get(id);

                    ISagaModel model = sagaRegistrator.FindModelByName(saga.ExecutionInfo.ModelName);

                    logger.LogInformation($"Tentando retomar o saga {id}");

                    bool isCompensating = saga.ExecutionState.IsCompensating;
                    var error = saga?.ExecutionState?.CurrentError;

                    try
                    {
                        await ExecuteSaga(
                            new EmptyEvent(),
                            model,
                            saga,
                            saga.Data.ID,
                            null,
                            true,
                            null);

                        logger.
                            LogInformation($"A saga {id} foi retomada");
                    }
                    catch (Exception ex)
                    {
                        // Fazer a exceção só mostrar quando a saga for retomada e houver um erro saga na retomada e sem problemas
                        // - Informação somente quando a saga compensar, então não mostra erro, somente informação.
                        var currentSaga = await sagaPersistance.Get(id);
                        var currentError = currentSaga?.ExecutionState?.CurrentError;

                        if (isCompensating)
                        {
                            if (error?.Message != currentError?.Message)
                            {
                                logger.LogError(ex, $"A saga {id} foi compensada, mas ocorreu um erro");
                            }
                            else
                            {
                                logger.LogInformation($"A saga {id} foi compensada");
                            }
                        }
                        else
                        {
                            logger.LogError(ex, $"A saga {id} foi retomada, mas ocorreu um erro");
                        }
                    }
                }
            }
            finally
            {
                isResuming--;
            }
        }
        public async Task Resume(Guid id)
        {
            isResuming++;
            try
            {
                List<string> invalidModels = new();

                ISaga saga = await sagaPersistance.Get(id);
                ISagaModel model = sagaRegistrator.FindModelByName(saga.ExecutionInfo.ModelName);

                if (model == null) { invalidModels.Add(saga.ExecutionInfo.ModelName); }

                if (invalidModels.Count > 0) { throw new Exception($"Modelos Saga {string.Join(", ", invalidModels.Distinct().ToArray())} não encontrados"); }

                await ExecuteSaga(
                    new EmptyEvent(),
                    model,
                    saga,
                    saga.Data.ID,
                    null,
                    true,
                    null);
            }
            finally
            {
                isResuming--;
            }
        }

        public Task<ISaga> Publish(
            ISagaEvent @event, TimeSpan? timeout = null)
        {
            return Publish(@event, null, timeout);
        }

        public Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, TimeSpan? timeout = null)
        {
            return Publish(@event, executionValues, new SagaRunOptions(), timeout);
        }

        public async Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, SagaRunOptions runOptions, TimeSpan? timeout = null)
        {
            return await Publish(@event, executionValues, null, runOptions, timeout);
        }

        public async Task<ISaga> Publish(ISagaEvent @event, IDictionary<string, object> executionValues, Guid? parentId, SagaRunOptions runOptions, TimeSpan? timeout = null)
        {
            runOptions ??= new SagaRunOptions();

            Type eventType = @event.GetType();
            SagaID sagaId = SagaID.From(@event.ID);

            ISagaModel model = sagaRegistrator.FindModelForEventType(eventType) ?? throw new SagaEventNotRegisteredException(eventType);

            SagaID? parentSagaId = parentId == null ? null : SagaID.From(parentId.Value);
            ISaga newSaga = await CreateNewSagaIfRequired(model, sagaId, parentSagaId, eventType);

            var st = Stopwatch.StartNew();
            var id = SagaID.From(newSaga?.Data?.ID ?? sagaId.Value);
            while (true)
            {
                try
                {
                    var createdSaga = await ExecuteSaga(
                        @event,
                        model,
                        newSaga,
                        id,
                        executionValues,
                        false,
                        runOptions);

                    return createdSaga;
                }
                catch (Acades.Saga.Exceptions.SagaIsBusyException ex)
                {
                    if (timeout == null || st.Elapsed > timeout || ex.Id != id) { throw; }
                    await Task.Delay(25);
                }
            }
        }

        public async Task WaitForIdle(
            Guid id, SagaWaitOptions waitOptions = null)
        {
            waitOptions ??= new SagaWaitOptions();

            try
            {
                bool isSagaInIdleState = false;

                messageBus.Subscribe<ExecutionEndMessage>(this, mesage =>
                {
                    if (mesage.Saga?.Data?.ID == id)
                    {
                        ISaga saga = sagaPersistance.Get(id).GetAwaiter().GetResult();
                        if (saga?.IsIdle() == true) { isSagaInIdleState = true; }
                    }
                    return Task.CompletedTask;
                });

                ISaga saga = await sagaPersistance.Get(id) ?? throw new SagaInstanceNotFoundException(id);

                if (saga.IsIdle()) { return; }

                Stopwatch stopwatch = Stopwatch.StartNew();
                while (!isSagaInIdleState)
                {
                    await Task.Delay(250);
                    if (stopwatch.Elapsed >= waitOptions.Timeout) { throw new TimeoutException(); }
                }
            }
            finally
            {
                messageBus.Unsubscribe<ExecutionEndMessage>(this);
            }
        }

        private async Task<ISaga> ExecuteSaga(
            ISagaEvent @event, ISagaModel model,
            ISaga saga,
            Guid sagaID,
            IDictionary<string, object> executionValues,
            bool executeResuming,
            SagaRunOptions runOptions)
        {
            bool sagaStarted = false;

            try
            {
                serviceProvider.
                    GetRequiredService<ObservableRegistrator>().
                    Initialize();

                await messageBus.Publish(new ExecutionStartMessage(saga ?? new Saga.Models.Saga { Data = new EmptySagaData { ID = sagaID } }, model));

                sagaStarted = true;

                saga ??= await sagaPersistance.Get(sagaID);

                if (saga == null) { throw new SagaInstanceNotFoundException(sagaID); }

                if (saga.ExecutionState.IsDeleted) { throw new CountNotExecuteDeletedSagaException(sagaID); }

                if (!executeResuming)
                {
                    if (runOptions != null) { saga.ExecutionState.CanBeResumed = runOptions.CanBeResumed; }

                    if (saga.IsIdle())
                    {
                        saga.ExecutionState.PrepareForExecution(@event);

                        saga.ExecutionValues.Set(executionValues);
                    }
                    else
                    {
                        throw new SagaNeedToBeResumedException(saga.Data.ID);
                    }

                    logger.LogInformation($"Executando a saga: {saga.Data.ID}");
                }
                else
                {
                    logger.LogInformation($"Retomando a saga: {saga.Data.ID}");
                }

                ExecuteActionCommandHandler handler = serviceProvider.GetRequiredService<ExecuteActionCommandHandler>();

                return await handler.Handle(new ExecuteActionCommand
                {
                    Async = AsyncExecution.False(),
                    Saga = saga,
                    Model = model
                });
            }
            catch (Exception ex)
            {
                if (sagaStarted)
                { await messageBus.Publish(new ExecutionEndMessage(saga ?? new Saga.Models.Saga { Data = new EmptySagaData { ID = sagaID } }, ex)); }

                if (ex is SagaStepException sagaStepException && sagaStepException?.OriginalException != null)
                {
                    sagaStepException.OriginalException.PreserveStackTrace();
                    throw sagaStepException.OriginalException;
                }

                throw;
            }
        }

        private async Task<ISaga> CreateNewSagaIfRequired(ISagaModel model, SagaID id, SagaID? parentId, Type eventType)
        {
            ISaga saga = null;

            if (eventType != null)
            {
                bool isStartEvent = model.Actions.IsStartEvent(eventType);

                if (isStartEvent) { saga = await CreateNewSaga(model, id, parentId); }
            }

            return saga;
        }

        private async Task<ISaga> CreateNewSaga(ISagaModel model, SagaID id, SagaID? parentId)
        {
            if (id == SagaID.Empty()) { id = SagaID.New(); }

            ISagaData data = (ISagaData)Activator.CreateInstance(model.SagaStateType);
            data.ID = id;

            ISaga saga = new Saga.Models.Saga
            {
                Data = data,
                ExecutionInfo = new SagaExecutionInfo
                {
                    ModelName = model.Name,
                    Created = dateTimeProvider.Now,
                    Modified = dateTimeProvider.Now
                },
                ExecutionState = new SagaExecutionState
                {
                    ParentID = parentId?.Value,
                    CurrentState = new SagaStartState().GetStateName(),
                    CurrentStep = null
                }
            };

            var existingProcessManager = await sagaPersistance.Get(id);
            if (existingProcessManager != null) { throw new Exception($"Saga com id {id} já existe"); }

            return saga;
        }

    }
    static class ExceptionHelper
    {
        private static Action<Exception> _preserveInternalException;

        static ExceptionHelper()
        {
            try
            {
                MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);
                _preserveInternalException = (Action<Exception>)Delegate.CreateDelegate(typeof(Action<Exception>), preserveStackTrace);
            }
            catch { }
        }

        public static void PreserveStackTrace(this Exception ex)
        {
            _preserveInternalException(ex);
        }
    }
}
