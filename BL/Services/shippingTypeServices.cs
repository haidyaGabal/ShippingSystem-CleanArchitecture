using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using DAL;
using DAL.Repositories;
using Domains;

namespace BL.Services
{


    public class ShippingTypeService : BaseService<TbShippingType, ShippingTypeDTO>, IShippingType
    {
        public ShippingTypeService(IRepository<TbShippingType> repo, IMapper mapper) : base(repo, mapper)
        {

        }
    }
}