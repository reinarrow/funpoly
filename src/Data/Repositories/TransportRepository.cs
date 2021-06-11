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
        private readonly ApplicationDbContext _applicationDbContext;

        public TransportRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(Transport type)
        {
            return await AddAsync(type);
        }

        public List<Transport> GetAll()
        {
            return _applicationDbContext.Transports.ToList();
        }

        public async Task<Transport> GetById(int id)
        {
            return await _applicationDbContext.Transports.SingleOrDefaultAsync(Transport => Transport.Id == id);
        }
    }
}