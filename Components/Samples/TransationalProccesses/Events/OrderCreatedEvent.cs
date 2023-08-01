using Acades.Saga.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransationalProccesses.Events
{
    public class OrderCreatedEvent : ISagaEvent
    {
        public Guid ID { get; set; }
    }
}
