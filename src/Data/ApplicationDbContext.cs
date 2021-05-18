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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Player> Players { get; set; }
    }
}