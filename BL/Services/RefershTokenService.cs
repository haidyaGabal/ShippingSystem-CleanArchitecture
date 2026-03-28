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
    public class RefershTokenService : BaseService<TbRefreshToken, RefershTokenDTO>, IRefershToken
    {
        IRepository<TbRefreshToken> _repo;
        AutoMapper.IMapper _mapper;
        public RefershTokenService(IRepository<TbRefreshToken> repo, AutoMapper.IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RefershTokenDTO> GenerateRefreshTokenAsync(string userId)
        {
            var refreshToken = new TbRefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(1),
                CreatedBy = Guid.Parse(userId),
                CurrentState = 1
            };

            await _repo.Add(refreshToken);


            return _mapper.Map<TbRefreshToken, RefershTokenDTO>(refreshToken);
        }

        public async Task<RefershTokenDTO?> GetByTokenAsync(string token)
        {
            var refreshToken = await _repo.FirstOrDefault(x => x.Token == token && x.CurrentState == 1);

            if (refreshToken == null)
                return null;

            return _mapper.Map<TbRefreshToken, RefershTokenDTO>(refreshToken);
        }

        public async Task<bool> Refresh(RefershTokenDTO refershTokenDTO)
        {
            /// can i use it when i need deactive all refracetoken 
            var tokenList =await _repo.GetListAsync(x => x.UserId == refershTokenDTO.UserId && x.CurrentState == 1);

            foreach (var token in tokenList)
            {
                _repo.ChangeStatus(refershTokenDTO.Id, 0);
            }

            var tokens = _mapper.Map<RefershTokenDTO, TbRefreshToken>(refershTokenDTO);
            _repo.Add(tokens);
            return true;
        }
    }

}

