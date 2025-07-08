namespace Core_Web_API.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Jersey { get; set; }
        public string Position { get; set; }
    }
}