using BL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts.Shipment
{
    public interface IAdminShipment
    {
        Task<PagedResult<ShipmentDTO>> GetAll(PaginationParams pagination, int? state);
        Task ChangeStatus(Guid id, int newState);
        Task<AdminShipmentDetailsDTO?> GetById(Guid id);
    }
}
