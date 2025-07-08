using System.ComponentModel.DataAnnotations;

namespace Core_Web_API.Models.DTOs
{
    public class LoginPlayerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}