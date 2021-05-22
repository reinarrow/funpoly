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
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(Game type)
        {
            return await AddAsync(type);
        }

        /// <summary>
        ///  Gets the unique game that should be available on the database (for now, might be more in future implementations)
        /// </summary>
        /// <returns></returns>
        public async Task<Game> GetAsync()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Games.FirstOrDefaultAsync();
        }

        public async Task<Game> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Games.SingleOrDefaultAsync(game => game.Id == id);
        }
    }
}