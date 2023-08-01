using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Persistance
{
    public interface ISagaPersistance
    {
        Task<ISaga> Get(Guid id);
        Task Remove(Guid id);
        Task Set(ISaga sagaData);
        Task<IList<Guid>> GetUnfinished();
    }
}
