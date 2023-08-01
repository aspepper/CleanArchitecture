using Acades.Saga.Models.Interfaces;
using System;

namespace Acades.Saga.Handlers.Events
{
    public class HandlersData : ISagaData
    {
        public Guid ID { get; set; }
    }
}
