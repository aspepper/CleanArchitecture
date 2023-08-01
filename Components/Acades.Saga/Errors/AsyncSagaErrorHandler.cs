using Acades.Saga.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Acades.Saga.Errors
{
    public class AsyncSagaErrorHandler : IAsyncSagaErrorHandler
    {
        public Task Handle(ISaga saga, Exception error)
        {
            return Task.CompletedTask;
        }
    }
}
