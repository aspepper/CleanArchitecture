using System;
using System.Collections.Generic;

namespace Acades.Saga.Models
{
    public interface ISagaExecutionValues : IDictionary<String, Object>
    {
        object Get(string name);
    }
}