using Acades.Saga.Errors;
using Acades.Saga.Exceptions;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Messages;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga;
using Acades.Saga.ModelsSaga.Actions;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;
using Acades.Saga.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Acades.Saga.Commands.Handlers
{
    internal class ExecuteActionCommandHandler
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IMessageBus messageBus;
        private readonly IAsyncSagaErrorHandler asyncErrorHandler;
        private readonly ILogger logger;

        public ExecuteActionCommandHandler(
            IServiceScopeFactory serviceScopeFactory,
            IMessageBus messageBus,
            IAsyncSagaErrorHandler errorHandler,
            ILogger logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.messageBus = messageBus;
            this.asyncErrorHandler = errorHandler;
            this.logger = logger;
        }

        public async Task<ISaga> Handle(ExecuteActionCommand command)
        {
            ISaga saga = command.Saga ?? throw new SagaInstanceNotFoundException(command.Model.SagaStateType);
            ISagaStep step = command.Model.Actions.FindStepForExecutionStateAndEvent(saga);

            ISagaAction action = command.Model.
                FindActionForStep(step);

            if (step.Async)
                saga.ExecutionState.AsyncExecution = AsyncExecution.True();

            ExecuteStepCommand executeStepCommand = new ExecuteStepCommand
            {
                Saga = saga,
                SagaStep = step,
                SagaAction = action,
                Model = command.Model
            };

            logger.
                LogDebug($"Saga: {saga.Data.ID}; Executing {(step.Async ? "async " : "")}step: {step.StepName}");

            if (step.Async)
            {
                DispatchStepAsync(executeStepCommand);
                return saga;
            }
            else
            {
                using IServiceScope scope = serviceScopeFactory.CreateScope();
                return await DispatchStepSync(scope.ServiceProvider, executeStepCommand);
            }
        }

        private void DispatchStepAsync(
            ExecuteStepCommand command)
        {
            Task.Run(async () =>
            {
                try
                {
                    using IServiceScope scope = serviceScopeFactory.CreateScope();
                    // quando o Saga é forçado a executar de forma assíncrona, ele pode ser retomado
                    if (command?.Saga?.ExecutionState != null)
                        command.Saga.ExecutionState.CanBeResumed = true;

                    await DispatchStepSync(scope.ServiceProvider, command);
                }
                catch (Exception ex)
                {
                    await asyncErrorHandler.Handle(command.Saga, ex);
                }
            });
        }

        private async Task<ISaga> DispatchStepSync(
            IServiceProvider serviceProvider,
            ExecuteStepCommand command)
        {
            ExecuteStepCommandHandler stepExecutor = ActivatorUtilities.
                CreateInstance<ExecuteStepCommandHandler>(serviceProvider);

            ISaga saga = await stepExecutor.Handle(command);

            if (saga == null)
            {
                await messageBus.Publish(new ExecutionEndMessage(command.Saga, command.Saga?.ExecutionState?.CurrentError));

                return null;
            }
            else
            {
                if (saga.IsIdle())
                {
                    await messageBus.Publish(new ExecutionEndMessage(saga, saga.ExecutionState?.CurrentError));

                    if (saga.HasError())
                    {
                        saga.ExecutionState.CurrentError.PreserveStackTrace();
                        throw saga.ExecutionState.CurrentError;
                    }

                    return saga;
                }
                else
                {
                    return await Handle(new ExecuteActionCommand()
                    {
                        Async = AsyncExecution.False(),
                        Saga = saga,
                        Model = command.Model
                    });
                }
            }
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
