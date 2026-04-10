using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ShipingPackageService
     : BaseService<TbShipingPackage, ShipingPackageDTO>, IShipingPackages
    {
        public ShipingPackageService(IRepository<TbShipingPackage> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

     
    }

}
