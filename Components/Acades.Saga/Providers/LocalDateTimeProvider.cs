using System;
using Acades.Saga.Providers.Interfaces;

namespace Acades.Saga.Providers
{
    public class LocalDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
