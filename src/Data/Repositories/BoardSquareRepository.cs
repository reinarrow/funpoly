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
    public class BoardSquareRepository : Repository<BoardSquare>, IBoardSquareRepository
    {
        public BoardSquareRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory) { }

        public async Task<(bool, string)> AddAsync(BoardSquare type)
        {
            return await AddAsync(type);
        }

        public List<BoardSquare> GetAll()
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return _applicationDbContext.BoardSquares.ToList();
        }

        public async Task<BoardSquare> GetById(int id)
        {
            using var _applicationDbContext = _ContextFactory.CreateDbContext();
            return await _applicationDbContext.BoardSquares.SingleOrDefaultAsync(BoardSquare => BoardSquare.Id == id);
        }
    }
}