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
        private readonly ApplicationDbContext _applicationDbContext;

        public ContinentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(Continent type)
        {
            return await AddAsync(type);
        }

        public List<Continent> GetAll()
        {
            return _applicationDbContext.Continents.ToList();
        }

        public async Task<Continent> GetById(int id)
        {
            return await _applicationDbContext.Continents.SingleOrDefaultAsync(Continent => Continent.Id == id);
        }
    }
}