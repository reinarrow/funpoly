using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Data.Repositories.Interfaces
{
    internal interface IBoardSquareRepository : IRepository<BoardSquare>
    {
        Task<(bool, string)> AddAsync(BoardSquare type);

        Task<BoardSquare> GetById(int id);

        List<BoardSquare> GetAll();
    }
}