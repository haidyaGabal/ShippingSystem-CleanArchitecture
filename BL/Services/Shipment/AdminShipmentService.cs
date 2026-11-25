using AutoMapper;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using DAL.Models;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class AdminShipmentService : IAdminShipment
    {
        private readonly IRepository<TbShipment> _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uow;
        private readonly IShipmentsStatus _shipmentsStatus;

        public AdminShipmentService(IRepository<TbShipment> repo, IMapper mapper,
            IUserService userService, IUnitOfWork uow, IShipmentsStatus shipmentsStatus)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
            _uow = uow;
            _shipmentsStatus = shipmentsStatus;
        }

        public async Task<PagedResult<ShipmentDTO>> GetAll(PaginationParams pagination, int? state)
        {
            var paged = await _repo.GetList(
                c => state == null ? c.CurrentState > 0 : c.CurrentState == state,
                pagination,
                c => c.Sender,
                c => c.Receiver);

            var items = _mapper.Map<List<TbShipment>, List<ShipmentDTO>>(paged.Items);
            return new PagedResult<ShipmentDTO>(items, paged.TotalCount, paged.PageNumber, paged.PageSize);
        }
        public async Task<AdminShipmentDetailsDTO?> GetById(Guid id)
        {
            var shipment = (await _repo.GetList(
                    c => c.Id == id && c.CurrentState > 0,
                    c => c.Sender,
                    c => c.Sender.City,
                    c => c.Receiver,
                    c => c.Receiver.City,
                    c => c.ShipingType,
                    c => c.TbShippmentStatuses))
                .FirstOrDefault();

            if (shipment == null) return null;

            return new AdminShipmentDetailsDTO
            {
                Shipment = _mapper.Map<ShipmentDTO>(shipment),
                SenderCity = shipment.Sender?.City?.CityEname?.Trim(),
                ReceiverCity = shipment.Receiver?.City?.CityEname?.Trim(),
                ShippingTypeName = shipment.ShipingType?.ShipingTypeEname,
                History = shipment.TbShippmentStatuses
                    .OrderBy(s => s.CreatedDate)
                    .Select(s => new ShipmentHistoryItemDTO
                    {
                        State = s.CurrentState,
                        Date = s.CreatedDate,
                        Notes = s.Notes
                    }).ToList()
            };
        }

        public async Task ChangeStatus(Guid id, int newState)
        {
            var adminId = _userService.GetLoggedInUser();

            var shipment = (await _repo.GetList(c => c.Id == id && c.CurrentState > 0)).FirstOrDefault();
            if (shipment == null)
                throw new InvalidOperationException("Shipment not found.");

            // only the next step, or Delivered -> Returned
            bool allowed = newState == shipment.CurrentState + 1
                           || (shipment.CurrentState == 5 && newState == 6);
            if (newState < 1 || newState > 6 || !allowed)
                throw new ArgumentException("This status change is not allowed.");

            try
            {
                await _uow.BeginTransactionAsync();

                if (!_repo.ChangeStatus(id, adminId, newState))
                    throw new InvalidOperationException("Shipment not found.");

                _shipmentsStatus.Add(new ShipmentStatusDTO { ShipmentId = id, CurrentState = newState });

                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}