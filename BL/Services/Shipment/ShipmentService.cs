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

        private readonly IMapper _mapper;

        public ShipmentService( IMapper mapper, IUserReceiver userReceiver, IUserSender userSender, ICalculateRate calculateRate,ITrackingNumber trackingNumber, IUnitOfWork uow) : base(uow, mapper)
        {
            _userReceiver = userReceiver;
            _userSender = userSender;
            _caculateRate = calculateRate;
            _rackingNumber = trackingNumber;
            _mapper = mapper;
     
            _uow = uow;
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

                ///save sender
                if (shipmentDTO.SenderId == Guid.Empty)
                {
                    Guid senderId = Guid.Empty;
                    _userSender.Add(shipmentDTO.userSender, out senderId);
                    shipmentDTO.SenderId = senderId;

                }
                ///save reciver
                if (shipmentDTO.ReceiverId == Guid.Empty)
                {
                    Guid reciverId = Guid.Empty;
                    _userReceiver.Add(shipmentDTO.userReceiver, out reciverId);
                    shipmentDTO.ReceiverId = reciverId;

                }
           
                await  this.Add(shipmentDTO, shipmentDTO.Id);
                await _uow.CommitTransactionAsync();
            }
            catch(Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
            
        }
    }

}
 

