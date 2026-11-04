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
    public class ShipmentService : BaseService<TbShipment, ShipmentDTO>, IShipment
    {


        private readonly IUserReceiver _userReceiver;
        private readonly IUserSender _userSender;
        private readonly ICalculateRate _caculateRate;
        private readonly ITrackingNumber _rackingNumber;
        private readonly IUnitOfWork _uow;
        IUserService _userService;

        private readonly IMapper _mapper;

        public ShipmentService(IMapper mapper, IUserReceiver userReceiver, IUserSender userSender, ICalculateRate calculateRate, ITrackingNumber trackingNumber, IUnitOfWork uow, IUserService userService) : base(uow, mapper, userService)
        {
            _userReceiver = userReceiver;
            _userSender = userSender;
            _caculateRate = calculateRate;
            _rackingNumber = trackingNumber;
            _mapper = mapper;

            _uow = uow;
            _userService = userService;
        }
     


        public async Task Create(ShipmentDTO shipmentDTO)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                ///calculate TrackingNumber
                shipmentDTO.TrackingNumber = _rackingNumber.CreateTrachingNumber(shipmentDTO);

                ///calculate Rate
                shipmentDTO.ShipingRate = _caculateRate.Calculate(shipmentDTO);

                var userId = _userService.GetLoggedInUser();
            
                ///save sender
                if (shipmentDTO.SenderId == Guid.Empty)
                {
                    Guid senderId = Guid.Empty;
                    shipmentDTO.Sender.UserId = userId;
                    _userSender.Add(shipmentDTO.Sender, out senderId);
                    shipmentDTO.SenderId = senderId;

                }
                ///save reciver
                if (shipmentDTO.ReceiverId == Guid.Empty)
                {
                    Guid reciverId = Guid.Empty;
                    shipmentDTO.Receiver.UserId = userId;
                    _userReceiver.Add(shipmentDTO.Receiver, out reciverId);
                    shipmentDTO.ReceiverId = reciverId;

                }

                Guid gShipmentId = Guid.Empty;
             

                 this.Add(shipmentDTO,out gShipmentId);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw new Exception();

            }

        }
        public async Task Edit(ShipmentDTO shipmentDTO)
        {
            if (shipmentDTO == null)
                throw new ArgumentNullException(nameof(shipmentDTO));
            if (shipmentDTO.Id == Guid.Empty)
                throw new ArgumentException("Shipment id is required.", nameof(shipmentDTO));
            if (shipmentDTO.Sender == null || shipmentDTO.Receiver == null)
                throw new ArgumentException("Sender and receiver are required.", nameof(shipmentDTO));

            try
            {
                await _uow.BeginTransactionAsync();

                var userId = _userService.GetLoggedInUser();

                // 1. Make sure the shipment exists and belongs to the logged-in user
                var existing = (await _repo.GetList(c => c.Id == shipmentDTO.Id && c.CreatedBy == userId))
                               .FirstOrDefault();
                if (existing == null)
                    throw new InvalidOperationException("Shipment not found.");

                // 2. Never trust ids/owner/tracking number coming from the client
                shipmentDTO.SenderId = existing.SenderId;
                shipmentDTO.ReceiverId = existing.ReceiverId;
                shipmentDTO.TrackingNumber = existing.TrackingNumber;
                shipmentDTO.Sender.Id = existing.SenderId;
                shipmentDTO.Receiver.Id = existing.ReceiverId;
                shipmentDTO.Sender.UserId = userId;
                shipmentDTO.Receiver.UserId = userId;

                // 3. Recalculate the rate
                shipmentDTO.ShipingRate = _caculateRate.Calculate(shipmentDTO);

                // 4. Update sender, receiver and shipment, and check each result
                if (!_userSender.Update(shipmentDTO.Sender))
                    throw new InvalidOperationException("Sender not found.");

                if (!_userReceiver.Update(shipmentDTO.Receiver))
                    throw new InvalidOperationException("Receiver not found.");

                if (!this.Update(shipmentDTO))
                    throw new InvalidOperationException("Shipment update failed.");

                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;   // keeps the original exception and stack trace
            }
        }

        public async Task<List<ShipmentDTO>> GetShipments()
        {
            try
            {
                var userId = _userService.GetLoggedInUser();
                var shipments =await _repo.GetList(c => 
                c.CreatedBy == userId,
                c => c.Sender,
                c => c.Receiver);


                return _mapper.Map<List<TbShipment>,List<ShipmentDTO>>(shipments);


            }
            catch (Exception ex)
            {
                throw new Exception();

            }

        }


        public async Task<PagedResult<ShipmentDTO>> GetShipments(PaginationParams pagination)
        {
            try
            {
                var userId = _userService.GetLoggedInUser();

                var pagedShipments = await _repo.GetList(
                    c => c.CreatedBy == userId&& c.CurrentState > 0,
                    pagination,
                    c => c.Sender,
                    c => c.Receiver);
                   

                var mappedItems = _mapper.Map<List<TbShipment>, List<ShipmentDTO>>(pagedShipments.Items);

                return new PagedResult<ShipmentDTO>(
                    mappedItems,
                    pagedShipments.TotalCount,
                    pagedShipments.PageNumber,
                    pagedShipments.PageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);   // was: throw new Exception();
            }
        }

        // return shipment based on user id
        public async Task<ShipmentDTO> GetShipment(Guid id)
        {
            try
            {
                var userId = _userService.GetLoggedInUser();
                var shipments = await _repo.GetList(c =>
                    c.CreatedBy == userId && c.Id == id,
                    c => c.Sender,
                    c => c.Sender.City,       // needed for S CountryId
                    c => c.Receiver,
                    c => c.Receiver.City);    // needed for R CountryId

                var shipment = shipments.FirstOrDefault();
                if (shipment == null) return null;

                return _mapper.Map<ShipmentDTO>(shipment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);   // was: "GETSHIPMENT-V2: " + ex
            }
        }
    }

}


