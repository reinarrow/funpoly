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
    public class ParcelRepository : Repository<Parcel>, IParcelRepository
    {
        public ParcelRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }
        public async Task<(bool, string)> AddAsync(Parcel type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Parcel>> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Parcels.ToListAsync();
        }

        public async Task<Parcel> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Parcels.SingleOrDefaultAsync(Parcel => Parcel.Id == id);
        }
    }
}