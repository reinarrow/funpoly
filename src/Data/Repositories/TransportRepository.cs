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
    public class TransportRepository : Repository<Transport>, ITransportRepository
    {
        public TransportRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(Transport type)
        {
            return await AddAsync(type);
        }

        public List<Transport> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return _applicationDbContext.Transports.ToList();
        }

        public async Task<Transport> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.Transports.SingleOrDefaultAsync(Transport => Transport.Id == id);
        }
    }
}