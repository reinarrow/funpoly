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
    public class ContinentRepository : Repository<Continent>, IContinentRepository
    {
        public ContinentRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(Continent type)
        {
            return await AddAsync(type);
        }

        public List<Continent> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return _applicationDbContext.Continents.ToList();
        }

        public async Task<Continent> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Continents.SingleOrDefaultAsync(Continent => Continent.Id == id);
        }
    }
}