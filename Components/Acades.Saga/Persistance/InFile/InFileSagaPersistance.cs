using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Messages;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.Serializer;

namespace Acades.Saga.Persistance.InFile
{
    public class InFileSagaPersistance : ISagaPersistance
    {
        private readonly IMessageBus messageBus;
        private readonly ISagaSerializer sagaSerializer;

        public InFileSagaPersistance(IMessageBus messageBus, ISagaSerializer sagaSerializer)
        {
            this.messageBus = messageBus;
            this.sagaSerializer = sagaSerializer;
        }

        public async Task<ISaga> Get(Guid id)
        {
            if (!File.Exists($"{id}.json")) { return null; }
            var json = File.ReadAllText($"{id}.json");
            var saga = (ISaga)sagaSerializer.Deserialize(json);
            await messageBus.Publish(new SagaAfterRetrivedMessage(saga));

            return saga;
        }

        public async Task<IList<Guid>> GetUnfinished()
        {
            return new List<Guid>();
        }

        public async Task Remove(Guid id)
        {
            if (File.Exists($"{id}.json")) { File.Delete($"{id}.json"); }
        }

        public async Task Set(ISaga saga)
        {
            if (File.Exists($"{saga.Data.ID}.json")) { File.Delete($"{saga.Data.ID}.json"); }

            var json = sagaSerializer.Serialize(saga);

            await messageBus.
                Publish(new SagaBeforeStoredMessage(saga));

            File.
                WriteAllText($"{saga.Data.ID}.json", json);
        }
    }
}
