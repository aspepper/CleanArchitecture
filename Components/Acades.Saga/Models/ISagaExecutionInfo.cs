using System;

namespace Acades.Saga.Models
{
    public interface ISagaExecutionInfo
    {
        DateTime Created { get;  }
        string ModelName { get;  }
        DateTime Modified { get;  }
    }
}