using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface ITeamRepository : IRepository<Team>
    {
        Task<(bool, string)> AddAsync(Team type);

        Task<Team> GetById(int id);

        Task<List<Team>> GetAll();
    }
}