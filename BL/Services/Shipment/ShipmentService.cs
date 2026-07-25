using AutoMapper;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
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


    }

}


