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
    public class RefershTokenService : BaseService<TbRefreshToken, RefreshTokenDTO>, IRefershToken
    {
        IRepository<TbRefreshToken> _repo;
        AutoMapper.IMapper _mapper;
        IUserService _userService;
        public RefershTokenService(IRepository<TbRefreshToken> repo, AutoMapper.IMapper mapper, IUserService userService)
            : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
        }

        //public async Task<RefreshTokenDTO> GenerateRefreshTokenAsync(string userId)
        //{
        //    var refreshToken = new TbRefreshToken
        //    {
        //        Token = Guid.NewGuid().ToString(),
        //        UserId = userId,
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        CreatedBy = Guid.Parse(userId),
        //        CurrentState = 1
        //    };

        //    await _repo.Add(refreshToken);


        //    return _mapper.Map<TbRefreshToken, RefreshTokenDTO>(refreshToken);
        //}


        public async Task<bool> Refresh(RefreshTokenDTO refershTokenDTO)
        {
            /// can i use it when i need deactive all refracetoken 
            var tokenList =await _repo.GetList(x => x.UserId == refershTokenDTO.UserId && x.CurrentState == 1);

            foreach (var token in tokenList)
            {
                _repo.ChangeStatus(refershTokenDTO.Id, _userService.GetLoggedInUser(), 0);
            }

            var tokens = _mapper.Map<RefreshTokenDTO, TbRefreshToken>(refershTokenDTO);
            _repo.Add(tokens);
            return true;

       
        }
    }

}

