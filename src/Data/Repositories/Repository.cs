using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Repositories.Interfaces;

namespace Funpoly.Data.Repositories
{
    /// <summary>
    /// Class-less CRUD Entity manager.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public (bool, string) Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            var rowsChanged = false;
            try
            {
                _applicationDbContext.Add(entity);
                rowsChanged = _applicationDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }

            //TODO Review null return
            return (rowsChanged, null);
        }

        public async Task<(bool, string)> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            var rowsChanged = false;
            try
            {
                await _applicationDbContext.AddAsync(entity);
                rowsChanged = await _applicationDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }

            //TODO Review null return
            return (rowsChanged, null);
        }

        public async Task<(bool, string)> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            var rowsChanged = false;
            try
            {
                _applicationDbContext.Update(entity);
                rowsChanged = await _applicationDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }

            //TODO Review null return
            return (rowsChanged, null);
        }

        public async Task<(bool, string)> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            var rowsChanged = false;
            try
            {
                _applicationDbContext.Remove(entity);
                rowsChanged = await _applicationDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }

            //TODO Review null return
            return (rowsChanged, null);
        }
    }
}