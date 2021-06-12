using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        List<TEntity> GetAll();

        List<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);

        (bool, string) Add(TEntity entity);

        Task<int> AddRangeAsync(List<TEntity> entities);

        int AddRange(List<TEntity> entities);

        Task<(bool, string)> AddAsync(TEntity entity);

        Task<(bool, string)> UpdateAsync(TEntity entity);

        Task<(bool, string)> RemoveAsync(TEntity entity);

        Task<bool> CheckIsEmptyAsync();

        Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> func);

        Task<TEntity> GetByIdAsync(int id);

        int GetCount();
    }
}