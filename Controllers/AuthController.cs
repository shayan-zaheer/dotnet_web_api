using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core_Web_API.Helpers;
using Core_Web_API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Core_Web_API.Models.Entities;
using AutoMapper;
using Core_Web_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace Core_Web_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreatePlayerDto createPlayerDto)
        {
            var player = await authService.RegisterAsync(createPlayerDto);
            if (player == null)
            {
                return BadRequest("User already exists!");
            }

            return Ok(player);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var result = await authService.RefreshTokensAsync(refreshTokenRequestDto);
            if (result is null || result.RefreshToken is null || result.AccessToken is null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPlayerDto loginPlayerDto)
        {
            var result = await authService.LoginAsync(loginPlayerDto);
            if (result is null)
            {
                return BadRequest("Invalid username or password!");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Left Winger,Right Winger")]
        [HttpGet("admin")]
        public IActionResult WingerOnly()
        {
            return Ok("You are a winger!");
        }
    }
}