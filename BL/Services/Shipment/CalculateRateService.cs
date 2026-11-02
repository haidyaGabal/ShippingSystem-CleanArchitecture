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
            const decimal baseFee = 20m;        
            const decimal perKg = 5m;             
            const decimal perCm3 = 0.0005m;      

           
            decimal volumetricKg = (decimal)(dTO.Length * dTO.Width * dTO.Height) / 5000m;
            decimal chargeableKg = Math.Max((decimal)dTO.Weight, volumetricKg);

            decimal rate = baseFee + chargeableKg * perKg;

           
            rate += dTO.PackageValue * 0.01m;

           
            return Math.Round(Math.Min(rate, 10000m), 2);
        }


    }
}
