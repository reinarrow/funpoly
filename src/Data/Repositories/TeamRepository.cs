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
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(Team type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Team>> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Teams.Include(t => t.Players).ToListAsync();
        }

        public async Task<Team> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Teams.SingleOrDefaultAsync(Team => Team.Id == id);
        }
    }
}