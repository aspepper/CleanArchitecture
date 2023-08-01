using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Acades.Saga.MessageBus;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ModelsSaga.Interfaces;
using Acades.Saga.Registrator.Interfaces;
using Acades.Saga.Utils;

namespace Acades.Saga.Registrator
{
    public class SagaRegistrator : ISagaRegistrator
    {
        private static readonly List<ISagaModel> registeredModels = new List<ISagaModel>();
        private static bool wasInitialized = false;

        private readonly IServiceProvider serviceProvider;
        private readonly IMessageBus messageBus;

        public SagaRegistrator(
            IMessageBus messageBus,
            IServiceProvider serviceProvider)
        {
            this.messageBus = messageBus;
            this.serviceProvider = serviceProvider;
            RegisterAllModelWithBuilders();
        }

        public void Register(ISagaModel model)
        {
            registeredModels.Add(model);
        }

        ISagaModel ISagaRegistrator.FindModelForEventType(Type eventType)
        {
            return registeredModels.
                FirstOrDefault(model => model.Actions.IsEventSupported(eventType));
        }

        ISagaModel ISagaRegistrator.FindModelByName(string name)
        {
            return registeredModels.
                FirstOrDefault(model => model.Name == name);
        }

        private void RegisterAllModelWithBuilders()
        {
            if (wasInitialized) { return; }

            Type[] modelBuildersTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.Is(typeof(ISagaModelBuilder<>))).ToArray();

            foreach (Type modelBuilderType in modelBuildersTypes)
            {
                object modelBuilder = ActivatorUtilities.CreateInstance(serviceProvider, modelBuilderType);

                ISagaModelBuilder<ISagaData> emptyBuildModel = null;
                string buildMethodName = nameof(emptyBuildModel.Build);
                var buildMethodInfo = modelBuilderType.GetMethod(buildMethodName, BindingFlags.Public | BindingFlags.Instance);
                ISagaModel model = (ISagaModel)buildMethodInfo.Invoke(modelBuilder, Array.Empty<object>());

                Register(model);
            }

            wasInitialized = true;
        }
    }
}
