using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data;
using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;

namespace Funpoly.Data.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<(bool, string)> AddAsync(Player type)
        {
            return await AddAsync(type);
        }

        public Player GetById(int id)
        {
            return GetAll().SingleOrDefault(player => player.Id == id);
        }
    }
}