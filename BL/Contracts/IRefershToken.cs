using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IRefershToken : IBaseService<TbRefreshToken, RefershTokenDTO>
    {
        
            Task<RefershTokenDTO> GenerateRefreshTokenAsync(string userId);

            Task<RefershTokenDTO?> GetByTokenAsync(string token);

        Task<bool> Refresh(RefershTokenDTO refershTokenDTO);



        //Task<bool> ValidateRefreshTokenAsync(string token);


        ///When user refreshes token (generate new one and revoke old one)
        // Task<RefershTokenDTO> RotateRefreshTokenAsync(string token);


        ///Used when user logs out.
        //Task<bool> RevokeRefreshTokenAsync(string token);

        // Task<List<TbRefreshToken>> GetUserRefreshTokensAsync(string userId);




    }
}
