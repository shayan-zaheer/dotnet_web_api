namespace Core_Web_API.Models.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Jersey { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}