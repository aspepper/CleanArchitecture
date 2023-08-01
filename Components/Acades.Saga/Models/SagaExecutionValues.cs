using System;
using System.Collections.Generic;

namespace Acades.Saga.Models
{
    public class SagaExecutionValues : Dictionary<String, Object>, ISagaExecutionValues
    {
        public object Get(string name)
        {
            this.TryGetValue(name, out object val);
            return val;
        }

        internal void Set(IDictionary<string, object> executionValues)
        {
            this.Clear();
            if (executionValues != null)
                foreach (var item in executionValues)
                    this[item.Key] = item.Value;
        }
    }
}