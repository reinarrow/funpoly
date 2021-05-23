using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Funpoly.Data.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        // TODO: delete method
        [Obsolete("This method are going to be deleted once the multigame is supported")]
        /// <summary>
        ///  Gets the unique game that should be available on the database (for now, might be more in future implementations)
        /// </summary>
        /// <returns></returns>
        public async Task<Game> GetAsync() 
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Games.FirstOrDefaultAsync();
        }
    }
}