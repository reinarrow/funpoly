using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data;
using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Funpoly.Data.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(Player type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Player>> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Players.ToListAsync();
        }

        public async Task<Player> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Players.SingleOrDefaultAsync(player => player.Id == id);
        }
    }
}