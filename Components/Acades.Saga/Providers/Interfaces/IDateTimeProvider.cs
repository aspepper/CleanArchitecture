using System;

namespace Acades.Saga.Providers.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
