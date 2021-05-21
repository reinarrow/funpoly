using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IGameRepository : IRepository<Game>
    {
        Task<(bool, string)> AddAsync(Game type);

        Task<Game> GetById(int id);

        List<Game> GetAll();
    }
}