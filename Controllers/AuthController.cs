using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core_Web_API.Helpers;
using Core_Web_API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Core_Web_API.Models.Entities;
using AutoMapper;
using Core_Web_API.Services.Interfaces;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPlayerDto loginPlayerDto)
        {
            var token = await authService.LoginAsync(loginPlayerDto);
            if (token is null)
            {
                return BadRequest("Invalid username or password!");
            }

            return Ok(token);
        }

       
    }
}