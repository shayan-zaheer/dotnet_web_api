using Core_Web_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core_Web_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Player> Players { get; set; }
    }
}