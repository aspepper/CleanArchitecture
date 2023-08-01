using Acades.Saga.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransationalProccesses
{
    public class OrderData : ISagaData
    {
        public OrderData()
        {

        }

        public Guid ID { get; set; }
    }

}
