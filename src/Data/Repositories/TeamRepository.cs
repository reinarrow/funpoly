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
        private readonly ApplicationDbContext _applicationDbContext;

        public TeamRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(Team type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Team>> GetAll()
        {
            return await _applicationDbContext.Teams.ToListAsync();
        }

        public async Task<Team> GetById(int id)
        {
            return await _applicationDbContext.Teams.SingleOrDefaultAsync(Team => Team.Id == id);
        }
    }
}