using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IPlayerRepository : IRepository<Player>
    {
        Player GetById(int id);
    }
}