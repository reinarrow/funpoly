using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IPlayerRepository : IRepository<Player>
    {
        Task<(bool, string)> AddAsync(Player type);

        Task<Player> GetById(int id);

        Task<List<Player>> GetAll();
    }
}