using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IRefreshTokenRetriver
    {

        public Task<RefreshTokenDTO> GetByToken(string token);
    }
}
