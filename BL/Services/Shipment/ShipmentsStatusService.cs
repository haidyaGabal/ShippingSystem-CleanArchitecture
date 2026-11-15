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
    public class ShipmentsStatusService : BaseService<TbShipmentStatus, ShipmentStatusDTO>, IShipmentsStatus
    {


        private readonly IUnitOfWork _uow;
        IUserService _userService;

        private readonly IMapper _mapper;

        public ShipmentsStatusService(IMapper mapper, IUnitOfWork uow, IUserService userService) : base(uow, mapper, userService)
        {
              _mapper = mapper;

            _uow = uow;
            _userService = userService;
        }
     


      
    }

}


