using System;
using System.Threading.Tasks;

namespace Acades.Saga.MessageBus.Interfaces
{
    public interface IMessageBus
    {
        Task Publish(IInternalMessage message);

        void Subscribe<T>(object listener, Func<T, Task> p);

        void Unsubscribe<T>(object listener);
    }
}
