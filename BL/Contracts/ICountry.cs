using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ICountry : IBaseService<TbCountry,CountryDTO>
    {
    }
}
