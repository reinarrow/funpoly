using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        List<TEntity> GetAll();
        (bool, string) Add(TEntity entity);
        Task<int> AddRangeAsync(List<TEntity> entities);
        int AddRange(List<TEntity> entities);
        Task<(bool, string)> AddAsync(TEntity entity);
        Task<(bool, string)> UpdateAsync(TEntity entity);
        Task<(bool, string)> RemoveAsync(TEntity entity);
        Task<bool> checkIsEmptyAsync();
    }
}