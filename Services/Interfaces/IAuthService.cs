using Core_Web_API.Models.DTOs;
using Core_Web_API.Models.Entities;

namespace Core_Web_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<object?> RegisterAsync(CreatePlayerDto createPlayerDto);
        Task<object?> LoginAsync(LoginPlayerDto loginPlayerDto);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto player);
    }
}