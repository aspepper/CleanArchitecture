using Microsoft.Extensions.DependencyInjection;

namespace Acades.Saga.Config
{
    public interface ITheSagaConfig
    {
        IServiceCollection Services { get; }
    }
}