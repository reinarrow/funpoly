using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Funpoly.Data.Repositories
{
    /// <summary>
    /// Class-less CRUD Entity manager.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly IDbContextFactory<ApplicationDbContext> _ContextFactory;

        public Repository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _ContextFactory = contextFactory;
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
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
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

        public async Task<int> AddRangeAsync(List<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException($"{nameof(AddRangeAsync)} entities must not be null or empty");
            }

            try
            {
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
                await _applicationDbContext.AddRangeAsync(entities);
                return await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {ex.Message}");
            }
        }

        public int AddRange(List<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException($"{nameof(AddRangeAsync)} entities must not be null or empty");
            }

            try
            {
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
                _applicationDbContext.AddRange(entities);
                return _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {ex.Message}");
            }
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
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
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
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
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
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
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

        public async Task<bool> checkIsEmptyAsync()
        {
            try
            {
                using var _applicationDbContext = _ContextFactory.CreateDbContext();
                return !await _applicationDbContext.Set<TEntity>().AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"could not fetch db: {ex.Message}");
            }
        }
    }
}