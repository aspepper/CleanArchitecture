using Acades.Saga.Builders;
using Acades.Saga.Commands.Handlers;
using Acades.Saga.Config;
using Acades.Saga.Coordinators;
using Acades.Saga.Errors;
using Acades.Saga.Locking;
using Acades.Saga.Locking.InMemory;
using Acades.Saga.MessageBus.InMemory;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.Observables.Registrator;
using Acades.Saga.Persistance;
using Acades.Saga.Persistance.InFile;
using Acades.Saga.Persistance.InMemory;
using Acades.Saga.Providers;
using Acades.Saga.Providers.Interfaces;
using Acades.Saga.Registrator;
using Acades.Saga.Registrator.Interfaces;
using Acades.Saga.Serializer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AcadesSaga.Tests")]

namespace Acades.Saga
{
    public static class Extensions
    {
        public static IServiceCollection AddSaga(
            this IServiceCollection services,
            Action<ITheSagaConfig> configAction = null)
        {
            services.AddSingleton<ObservableRegistrator>();
            services.AddTransient<ISagaSerializer, SagaSerializer>();
            services.AddSingleton<IMessageBus, InMemoryMessageBus>();
            services.AddSingleton<ISagaPersistance, InMemorySagaPersistance>();
            services.AddSingleton<ISagaLocking, InMemorySagaLocking>();
            services.AddSingleton<IDateTimeProvider, LocalDateTimeProvider>();
            services.AddSingleton<IAsyncSagaErrorHandler, AsyncSagaErrorHandler>();
            services.AddSingleton<ILogger>(r =>
            {
                var loggerFactory = r.GetService<ILoggerFactory>();
                if (loggerFactory != null) { return loggerFactory.CreateLogger("AcadesSaga"); }
                return new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider().CreateLogger("AcadesSaga");
            });
            services.AddTransient<ISagaRegistrator, SagaRegistrator>();
            services.AddTransient<ISagaCoordinator, SagaCoordinator>();

            services.AddTransient(typeof(ISagaBuilder<>), typeof(SagaBuilder<>));
            services.AddTransient<ExecuteActionCommandHandler>();
            services.AddTransient<ExecuteStepCommandHandler>();
            services.AddTransient<CalculateNextStepHandler>();

            services.AddSagaModelDefinitions();

            if (configAction != null)
                configAction(new TheSagaConfig
                {
                    Services = services
                });

            return services;
        }

        public static void AddBeforeExecuteMiddlewares<T>(
            this ITheSagaConfig config)
            where T : ISagaMiddleware
        {
            if (config is null) { throw new ArgumentNullException(nameof(config)); }

            Middlewares.AddBeforeExecuteMiddlewares<T>();
        }

        public static void AddAfterExecuteMiddlewares<T>(
            this ITheSagaConfig config)
            where T : ISagaMiddleware
        {
            if (config is null) { throw new ArgumentNullException(nameof(config)); }

            Middlewares.AddAfterExecuteMiddlewares<T>();
        }

        public static void AddBeforeRequestCallback<T>(
            this ITheSagaConfig config)
            where T : ISagaBeforeRequestCallback
        {
            if (config is null) { throw new ArgumentNullException(nameof(config)); }

            Callbacks.AddBeforeRequestCallback<T>();
        }

        public static void AddAfterRequestCallback<T>(
            this ITheSagaConfig config)
            where T : ISagaAfterRequestCallback
        {
            if (config is null) { throw new ArgumentNullException(nameof(config)); }

            Callbacks.AddAfterRequestCallback<T>();
        }

        public static ITheSagaConfig AddModelDefinitions(
            this ITheSagaConfig config)
        {
            config.Services.AddSagaModelDefinitions();
            return config;
        }

        public static IServiceCollection AddSagaModelDefinitions(
            this IServiceCollection services)
        {
            services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ISagaModelBuilder<>)))
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());

            return services;
        }

        public static async Task<IServiceProvider> ResumeSagas(
            this IServiceProvider provider)
        {
            ISagaCoordinator coordinator = provider.
                GetRequiredService<ISagaCoordinator>();

            await coordinator.ResumeAll();

            return provider;
        }
        public static void UseFilePersistance(this ITheSagaConfig config)
        {
            config.Services.AddTransient<ISagaPersistance, InFileSagaPersistance>();
        }
    }
}
