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
    public class CounteryService: BaseService<TbCountry, CountryDTO>, ICountry
    {
        public CounteryService(IRepository<TbCountry> repo, IMapper mapper) : base(repo, mapper)
        {

        }
    }
}
