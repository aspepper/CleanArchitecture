using Acades.Saga.Activities;
using Acades.Saga.ExecutionContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransationalProccesses.Activities
{
    internal class CarRentalEvent : ISagaActivity<OrderData>
    {
        public async Task Compensate(IExecutionContext<OrderData> context)
        {
        }

        public async Task Execute(IExecutionContext<OrderData> context)
        {
        }
    }
}
