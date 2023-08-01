using Microsoft.Extensions.DependencyInjection;

namespace Acades.Saga.Config
{
    public class TheSagaConfig : ITheSagaConfig
    {
        public IServiceCollection Services { get; internal set; }
    }
}