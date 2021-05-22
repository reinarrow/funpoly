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
    public class PostcardRepository : Repository<Postcard>, IPostcardRepository
    {
        public PostcardRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }
        public async Task<(bool, string)> AddAsync(Postcard type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Postcard>> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Postcards.ToListAsync();
        }

        public async Task<Postcard> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Postcards.SingleOrDefaultAsync(Postcard => Postcard.Id == id);
        }
    }
}