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

        public List<Game> GetAll()
        {
            return _applicationDbContext.Games.ToList();
        }

        public async Task<Game> GetById(int id)
        {
            return await _applicationDbContext.Games.SingleOrDefaultAsync(game => game.Id == id);
        }
    }
}