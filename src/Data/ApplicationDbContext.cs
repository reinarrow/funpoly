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

            //builder.Entity<Player>().HasOne(player => player.Team).WithMany(team => team.Players).HasForeignKey(p => p.TeamId);
        }

        public DbSet<BoardSquare> BoardSquares { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Postcard> Postcards { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<SurpriseCard> SurpriseCards { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}