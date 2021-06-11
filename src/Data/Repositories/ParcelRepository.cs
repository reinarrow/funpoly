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
        private readonly ApplicationDbContext _applicationDbContext;

        public ParcelRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(Parcel type)
        {
            return await AddAsync(type);
        }

        public async Task<List<Parcel>> GetAll()
        {
            return await _applicationDbContext.Parcels.ToListAsync();
        }

        public async Task<Parcel> GetById(int id)
        {
            return await _applicationDbContext.Parcels.SingleOrDefaultAsync(Parcel => Parcel.Id == id);
        }
    }
}