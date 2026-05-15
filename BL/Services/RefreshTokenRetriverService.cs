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
    public class RefreshTokenRetriverService : IRefreshTokenRetriver
    {
        IRepository<TbRefreshToken> _repo;
        IMapper _mapper;
        public RefreshTokenRetriverService(IRepository<TbRefreshToken> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public RefreshTokenDTO? GetByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var entity = _repo.FirstOrDefault(a => a.Token == token);

            if (entity == null || entity.Expires < DateTime.UtcNow)
                return null;

            return _mapper.Map<RefreshTokenDTO>(entity);
        }


    }
}
