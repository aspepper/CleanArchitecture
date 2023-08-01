using System;
using System.Collections.Generic;

namespace Acades.Saga.Models
{
    public interface IStepExecutionValues : IDictionary<String, Object>
    {
        object Get(string name);
    }
}