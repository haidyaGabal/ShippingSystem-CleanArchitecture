using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShipmentHistoryItemDTO
    {
        public int State { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }

    public class AdminShipmentDetailsDTO
    {
        public ShipmentDTO Shipment { get; set; } = null!;
        public string? SenderCity { get; set; }
        public string? ReceiverCity { get; set; }
        public string? ShippingTypeName { get; set; }
        public List<ShipmentHistoryItemDTO> History { get; set; } = new();
    }
}