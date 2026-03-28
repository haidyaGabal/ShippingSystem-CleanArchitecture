using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IRefershToken _RefreshTokenService;
        public AuthController(TokenService tokenService,
                              IUserService userService,
                              IRefershToken refreshTokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _RefreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO request)
        {
            var userResult = await _userService.LoginAsync(request);
            if (!userResult.Success)
            {
                return Unauthorized("Invalid credentials");
            }


            var userData = await GetClims(request.Email);
            var claims = userData.Item1;
            UserDTO user = userData.Item2;
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var storedToken = new RefershTokenDTO
            {
                Token = refreshToken,
                UserId = user.Id.ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CurrentState = 1
            };
            ///you must sure (storedToken) all token cu_st=0 before send 
            _RefreshTokenService.Refresh(storedToken);

            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = storedToken.Expires
            });
            /// you must sure when login this user is ok
            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-access-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("No refresh token found");
            }

            /// Retrieve the refresh token from the database
            /// here, when access token is expierd and you need to access for refToken from database to generate a new access token 
            var storedToken =await _RefreshTokenService.GetByTokenAsync(refreshToken);
            if (storedToken == null || storedToken.CurrentState == 0 || storedToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            /// Generate a new access token
            var claims = await GetClimsById(storedToken.UserId);

            var newAccessToken = _tokenService.GenerateAccessToken(claims);

            return Ok(new { AccessToken = newAccessToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("No refresh token found");
            }

            /// Retrieve the refresh token from the database
            var storedToken =await _RefreshTokenService.GetByTokenAsync(refreshToken);
            if (storedToken == null || storedToken.CurrentState == 0 || storedToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            /// Generate a new refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newRefreshDto = new RefershTokenDTO
            {
                Token = newRefreshToken,
                UserId = storedToken.Id.ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CurrentState = 1
            };
            _RefreshTokenService.Refresh(newRefreshDto);

            /// Set the new refresh token in the cookies
            Response.Cookies.Append("RefreshToken", newRefreshToken, new CookieOptions
            {
                /// RefreshToken is stored in httponly even if frontend not send to you can restore from it  
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new { RefreshToken = newRefreshToken });
        }



        /// <summary>
        /// GetClims by Id or by Email 
        /// </summary>
  
        async Task<(Claim[], UserDTO)> GetClims(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            return (claims, user);
        }

        async Task<Claim[]> GetClimsById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            return claims;
        }
    }
}
