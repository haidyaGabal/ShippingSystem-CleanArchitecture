using BL.Contracts.Shipment;
using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CalculateRateService : ICalculateRate
    {
        public CalculateRateService()
        {

        }

        public decimal Calculate(ShipmentDTO dTO)
        {
            return 4000;
        }

       
    }
}
