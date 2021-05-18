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
        private readonly ApplicationDbContext _applicationDbContext;

        public PostcardRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(Postcard type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Postcard>> GetAll()
        {
            return await _applicationDbContext.Postcards.ToListAsync();
        }

        public async Task<Postcard> GetById(int id)
        {
            return await _applicationDbContext.Postcards.SingleOrDefaultAsync(Postcard => Postcard.Id == id);
        }
    }
}