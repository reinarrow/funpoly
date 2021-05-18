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
        private readonly ApplicationDbContext _applicationDbContext;

        public BoardSquareRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<(bool, string)> AddAsync(BoardSquare type)
        {
            return await AddAsync(type);
        }

        public List<BoardSquare> GetAll()
        {
            return _applicationDbContext.BoardSquares.ToList();
        }

        public async Task<BoardSquare> GetById(int id)
        {
            return await _applicationDbContext.BoardSquares.SingleOrDefaultAsync(BoardSquare => BoardSquare.Id == id);
        }
    }
}