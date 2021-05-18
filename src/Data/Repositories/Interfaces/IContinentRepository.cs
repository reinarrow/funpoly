using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IContinentRepository : IRepository<Continent>
    {
        Task<(bool, string)> AddAsync(Continent type);

        Task<Continent> GetById(int id);

        List<Continent> GetAll();
    }
}