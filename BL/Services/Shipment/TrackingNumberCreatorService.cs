using BL.Contracts.Shipment;
using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class TrackingNumberCreatorService :ITrackingNumber
    {
        public TrackingNumberCreatorService()
        {

        }

        public double CreateTrachingNumber(ShipmentDTO dTO)
        {
            return 4000;
        }


        //public string CreateTrackingNumber(ShipmentDTO shipmentDTO)
        //{
        //    // Format: TRK + YYYYMMDD + Random + SenderInitials
        //    string datePart = DateTime.Now.ToString("yyyyMMdd");
        //    string randomPart = new Random().Next(1000, 9999).ToString();
        //    string senderInitials = shipmentDTO.userSender?.SenderName?.Substring(0, Math.Min(2, shipmentDTO.userSender?.SenderName?.Length ?? 2)) ?? "XX";

        //    return $"TRK-{datePart}-{randomPart}-{senderInitials}".ToUpper();
        //}
    }
}
