using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.ModelsSaga.Interfaces
{
    public interface ISagaModelBuilder<TSagaData>
        where TSagaData : ISagaData
    {
        ISagaModel Build();
    }

}
