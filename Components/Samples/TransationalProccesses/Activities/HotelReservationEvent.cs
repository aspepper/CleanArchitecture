using Acades.Saga.Activities;
using Acades.Saga.ExecutionContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransationalProccesses.Activities
{
    internal class HotelReservationEvent : ISagaActivity<OrderData>
    {
        public Task Compensate(IExecutionContext<OrderData> context)
        {
            return Task.CompletedTask;
        }

        public Task Execute(IExecutionContext<OrderData> context)
        {
            return Task.CompletedTask;
        }
    }
}
