using System.ComponentModel.DataAnnotations;

namespace Core_Web_API.Models.DTOs
{
    public class CreatePlayerDto
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Range(1, 20)]
        public int Jersey { get; set; }

        [Required]
        public string Position { get; set; }
    }
}