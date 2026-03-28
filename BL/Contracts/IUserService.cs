using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IUserService
    {
        Task<UserResultDTO>RegisterAsync(UserDTO RegisterDto);
        Task<UserResultDTO> LoginAsync(UserDTO LoginDto);
        Task LogoutAsync();
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<UserDTO> GetUserByEmailAsync(string userId);
       
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

    }
}
