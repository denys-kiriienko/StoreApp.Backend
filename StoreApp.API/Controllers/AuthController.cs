using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Shared.Constants;
using StoreApp.Shared.Dtos;
using StoreApp.Shared.Enums;
using StoreApp.Shared.Models;
using StoreApp.Shared.Services;
using System.Net;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CredentialsDto model)
        {
            var userModel = _mapper.Map<UserModel>(model);
            userModel.Role = UserRole.User;

            var result = await _authService.RegisterUserAsync(userModel);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsDto model)
        {
            var userModel = _mapper.Map<UserModel>(model);
            userModel.Role = UserRole.User;

            var (accessToken, refreshToken) = await _authService.LoginUserAsync(userModel);
            if (accessToken == null || refreshToken == null)
            {
                return Unauthorized("Invalid credentials");
            }

            Response.Cookies.Append(AuthConstants.AccessTokenCookie, accessToken, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(AuthConstants.AccessTokenExpiresMinutes)
            });

            Response.Cookies.Append(AuthConstants.RefreshTokenCookie, refreshToken, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(AuthConstants.RefreshTokenExpiresDays)
            });

            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue(AuthConstants.RefreshTokenCookie, out var oldRefreshToken))
            {
                return BadRequest(new { message = "Refresh token not found" });
            }

            var (accessToken, refreshToken) = await _authService.RefreshTokenAsync(oldRefreshToken);
            if (accessToken is null || refreshToken is null)
            {
                return Unauthorized("Invalid credentials");
            }

            Response.Cookies.Append(AuthConstants.AccessTokenCookie, accessToken, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(AuthConstants.AccessTokenExpiresMinutes)
            });

            Response.Cookies.Append(AuthConstants.RefreshTokenCookie, refreshToken, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(AuthConstants.RefreshTokenExpiresDays)
            });

            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(AuthConstants.AccessTokenCookie);
            Response.Cookies.Delete(AuthConstants.RefreshTokenCookie);

            return NoContent();
        }
    }
}
