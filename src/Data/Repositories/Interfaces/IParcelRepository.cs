using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IParcelRepository : IRepository<Parcel>
    {
        Task<(bool, string)> AddAsync(Parcel type);

        Task<Parcel> GetById(int id);

        Task<List<Parcel>> GetAll();
    }
}