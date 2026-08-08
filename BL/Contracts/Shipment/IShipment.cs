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
    public interface IShipment : IBaseService<TbShipment, ShipmentDTO>
    {
        public Task Create(ShipmentDTO shipmentDTO);

        public Task<List<ShipmentDTO>> GetShipments();
        public  Task<PagedResult<ShipmentDTO>> GetShipments(PaginationParams pagination);
        public Task<ShipmentDTO> GetShipment(Guid id);
    }
}
