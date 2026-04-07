using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using DAL;
using DAL.Repositories;
using Domains;

namespace BL.Services
{


    public class ShippingTypeService : BaseService<TbShipingType, ShipingTypeDTO>, IShipingType
    {
        public ShippingTypeService(IRepository<TbShipingType> repo, IMapper mapper) : base(repo, mapper)
        {

        }
    }
}