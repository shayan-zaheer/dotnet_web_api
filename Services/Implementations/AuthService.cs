using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Core_Web_API.Data;
using Core_Web_API.Models.DTOs;
using Core_Web_API.Models.Entities;
using Core_Web_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace Core_Web_API.Services.Implementations
{
    public class AuthService(AppDbContext context, IConfiguration configuration, IMapper _mapper) : IAuthService
    {
        public async Task<object?> LoginAsync(LoginPlayerDto loginPlayerDto)
        {
            var player = await context.Players.FirstOrDefaultAsync(u => u.Email == loginPlayerDto.Email);

            if (player == null)
            {
                return null;
            }

            if (new PasswordHasher<Player>().VerifyHashedPassword(player, player.PasswordHash, loginPlayerDto.Password) == PasswordVerificationResult.Failed)
            {
                return "Invalid credentials";
            }

            var response = new TokenResponseDto
            {
                AccessToken = CreateToken(player),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(player)
            };

            return response;
        }

        public async Task<object?> RegisterAsync(CreatePlayerDto createPlayerDto)
        {
            if (await context.Players.AnyAsync(u => u.Email == createPlayerDto.Email))
            {
                return null;
            }

            var player = _mapper.Map<Player>(createPlayerDto);
            var hashedPassword = new PasswordHasher<Player>().HashPassword(player, createPlayerDto.Password);

            player.PasswordHash = hashedPassword;

            context.Players.Add(player);
            await context.SaveChangesAsync();

            var result = _mapper.Map<ReadPlayerDto>(player);

            return result;
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var player = await ValidateRefreshTokenAsync
            (refreshTokenRequestDto.UserId, refreshTokenRequestDto.RefreshToken);

            if (player is null)
            {
                return null;
            }

             var response = new TokenResponseDto
            {
                AccessToken = CreateToken(player),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(player)
            };

            return response;
        }
        private string CreateToken(Player player)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, player.Name),
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                new Claim(ClaimTypes.Role, player.Position)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private async Task<Player?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var player = await context.Players.FindAsync(userId);
            if (player is null || player.RefreshToken != refreshToken || player.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return player;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(Player player)
        {
            var refreshToken = GenerateRefreshToken();
            player.RefreshToken = refreshToken;
            player.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }
    }
}