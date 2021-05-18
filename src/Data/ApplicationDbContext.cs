using Microsoft.EntityFrameworkCore;
using Funpoly.Data.Models;

namespace Funpoly.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
              new Player { Id = 1, Name = "Player 1" },
              new Player { Id = 2, Name = "Player 2" },
              new Player { Id = 3, Name = "Player 3" }
            );
        }

        public DbSet<Player> Player { get; set; }
    }
}