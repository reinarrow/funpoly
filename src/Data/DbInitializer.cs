using System;
using Funpoly.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Funpoly.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //Migrations

            applicationDbContext.Database.Migrate();

            // Seed data

            if (!applicationDbContext.Players.Any())
            {
                // Players
                applicationDbContext.Players.Add(
                    new Player
                    {
                        Name = "Agustin"
                    });
                applicationDbContext.Players.Add(
                    new Player
                    {
                        Name = "Lucas"
                    });
                applicationDbContext.Players.Add(
                    new Player
                    {
                        Name = "Rodolfa"
                    });
                applicationDbContext.SaveChanges();
            }
        }
    }
}