using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acades.Saga.Models.History;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Steps.Interfaces;

namespace Acades.Saga
{
    internal static class Callbacks
    {
        static readonly List<Type> beforeRequestCallbacks = new();
        static readonly List<Type> afterRequestCallbacks = new();

        public static void AddBeforeRequestCallback<T>()
            where T : ISagaBeforeRequestCallback
        {
            if (!beforeRequestCallbacks.Contains(typeof(T)))
                beforeRequestCallbacks.Add(typeof(T));
        }

        public static void AddAfterRequestCallback<T>()
            where T : ISagaAfterRequestCallback
        {
            if (!afterRequestCallbacks.Contains(typeof(T)))
                afterRequestCallbacks.Add(typeof(T));
        }

        public static async Task ExecuteBeforeRequestCallbacks(IServiceProvider serviceProvider, ISaga saga)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            using var beforeStartCallbackExecuter = new BeforeRequestCallbackExecuter(
                beforeRequestCallbacks, scope.ServiceProvider);

            MiddlewaresChain middlewaresChain = Middlewares.BuildSimpleChain(
                scope.ServiceProvider,
                beforeStartCallbackExecuter.Execute);

            await Middlewares.ExecuteChain(
                middlewaresChain,
                saga, null, null);
        }
        public static async Task ExecuteAfterRequestCallbacks(IServiceProvider serviceProvider, ISaga saga, Exception ex)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            using var beforeStartCallbackExecuter = new AfterRequestCallbackExecuter(
                afterRequestCallbacks, scope.ServiceProvider, ex);

            MiddlewaresChain middlewaresChain = Middlewares.BuildSimpleChain(
                scope.ServiceProvider,
                beforeStartCallbackExecuter.Execute);

            await Middlewares.ExecuteChain(
                middlewaresChain,
                saga, null, null);
        }
    }
    internal class BeforeRequestCallbackExecuter : IDisposable
    {
        List<Type> beforeRequestCallbacks;
        IServiceProvider serviceProvider;

        public BeforeRequestCallbackExecuter(List<Type> beforeRequestCallbacks, IServiceProvider serviceProvider)
        {
            this.beforeRequestCallbacks = beforeRequestCallbacks;
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            beforeRequestCallbacks = null;
            serviceProvider = null;
        }

        public async Task Execute(ISaga saga, ISagaStep sagaStep, StepData stepData)
        {
            foreach (var type in beforeRequestCallbacks)
            {
                var callback = ActivatorUtilities.CreateInstance(serviceProvider, type) as ISagaBeforeRequestCallback;
                await callback.InvokeAsync(saga);
            }
        }
    }

    internal class AfterRequestCallbackExecuter : IDisposable
    {
        List<Type> afterRequestCallbacks;
        IServiceProvider serviceProvider;
        Exception error;
        public AfterRequestCallbackExecuter(List<Type> afterRequestCallbacks, IServiceProvider serviceProvider, Exception error)
        {
            this.afterRequestCallbacks = afterRequestCallbacks;
            this.serviceProvider = serviceProvider;
            this.error = error;
        }

        public void Dispose()
        {
            afterRequestCallbacks = null;
            serviceProvider = null;
        }

        public async Task Execute(ISaga saga, ISagaStep sagaStep, StepData stepData)
        {
            foreach (var type in afterRequestCallbacks)
            {
                var callback = ActivatorUtilities.CreateInstance(serviceProvider, type) as ISagaAfterRequestCallback;
                await callback.InvokeAsync(saga, error);
            }
        }
    }
    public interface ISagaBeforeRequestCallback
    {
        Task InvokeAsync(ISaga saga);
    }
    public interface ISagaAfterRequestCallback
    {
        Task InvokeAsync(ISaga saga, Exception error);
    }
}
