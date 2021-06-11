using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IPostcardRepository : IRepository<Postcard>
    {
        Task<(bool, string)> AddAsync(Postcard type);

        Task<Postcard> GetById(int id);

        Task<List<Postcard>> GetAll();
    }
}