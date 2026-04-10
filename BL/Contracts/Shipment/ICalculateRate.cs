using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts.Shipment
{
    public interface ICalculateRate
    {
        public decimal Calculate(ShipmentDTO dTO);
    }
}
