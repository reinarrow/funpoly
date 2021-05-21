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
        private readonly ApplicationDbContext _applicationDbContext;

        public GameRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

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
            return await _applicationDbContext.Games.FirstOrDefaultAsync();
        }

        public async Task<Game> GetById(int id)
        {
            return await _applicationDbContext.Games.SingleOrDefaultAsync(game => game.Id == id);
        }
    }
}