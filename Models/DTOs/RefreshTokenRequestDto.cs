namespace Core_Web_API.Models.DTOs
{
    public class RefreshTokenRequestDto
    {
        public required Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}