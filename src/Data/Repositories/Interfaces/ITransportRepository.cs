using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface ITransportRepository : IRepository<Transport>
    {
        Task<(bool, string)> AddAsync(Transport type);

        Task<Transport> GetById(int id);

        List<Transport> GetAll();
    }
}