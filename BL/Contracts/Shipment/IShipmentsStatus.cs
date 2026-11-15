using BL.DTOs;
using DAL.Models;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts.Shipment
{
    public interface IShipmentsStatus : IBaseService<TbShipmentStatus, ShipmentStatusDTO>
    {
        
    }
}
