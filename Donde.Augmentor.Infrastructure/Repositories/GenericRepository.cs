using System;
using System.Linq;
using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IDondeModel
    {
        private readonly DondeContext _dbContext;
        public GenericRepository(DondeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            DetachLocal(entity);

            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        private void DetachLocal<T>(T entity)
            where T : class, IDondeModel
        {
            var local = _dbContext.Set<TEntity>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
