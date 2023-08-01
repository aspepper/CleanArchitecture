using Acades.Saga.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Acades.Saga.Errors
{
    public interface IAsyncSagaErrorHandler
    {
        Task Handle(ISaga saga, Exception error);
    }
}
