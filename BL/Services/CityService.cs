using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using BL.Mapping;
using DAL.Repositories;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CityService : BaseService<TbCity, CityDTO>, ICity
    {
        public CityService(IRepository<TbCity> repo, AutoMapper.IMapper mapper)
            : base(repo, mapper)
        {
        }

        public List<CityDTO> GetByCountryId(Guid countryId)
        {
            var cities = _repo.GetAll()
                              .Where(c => c.CountryId == countryId)
                              .ToList();

            return _mapper.Map<List<CityDTO>>(cities);
        }
    }

}

