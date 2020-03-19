using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        protected readonly DondeContext _dbContext;
        public GenericRepository(DondeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IDondeModel
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : class, IDondeModel
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(Guid id, TEntity entity) where TEntity : class, IDondeModel, IAuditFieldsModel
        {
            DetachLocal(entity);

            entity.UpdatedDate = DateTime.UtcNow;

            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync<TEntity>(id);
        }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IDondeModel, IAuditFieldsModel
        {
            entity.IsDeleted = false;
            entity.AddedDate = DateTime.UtcNow;

            SetAuditPropertiesOnChildCollectionsOrThrow(entity);

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        private void DetachLocal<TEntity>(TEntity entity) where TEntity : class, IDondeModel
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

        private TEntity SetAuditPropertiesOnChildCollectionsOrThrow<TEntity>(TEntity entity) where TEntity : class, IDondeModel, IAuditFieldsModel
        {
            var nestedCollectionsPropertyInfo = GetNestedCollectionsPropertyInfo(entity);

            foreach (var collectionPropertyInfo in nestedCollectionsPropertyInfo)
            {
                var childObjects = collectionPropertyInfo.GetValue(entity, null);
                if (childObjects != null)
                {
                    var childObjectsCasted = childObjects as IEnumerable<IAuditFieldsModel>;
                    if (childObjectsCasted == null)
                    {
                        throw new InvalidOperationException("Child collections property type must implement IDondeModelModel interface");
                    }

                    foreach (var eachChild in childObjectsCasted)
                    {
                        eachChild.IsDeleted = false;
                        eachChild.AddedDate = DateTime.UtcNow;                
                    }
                }
            }
            return entity;
        }

        private IEnumerable<PropertyInfo> GetNestedCollectionsPropertyInfo(object entity)
        {
            var nestedCollectionsPropertyInfo = entity.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x =>
                    x.PropertyType.IsGenericType && 
                    x.PropertyType.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)));

            return nestedCollectionsPropertyInfo;
        }
    }
}
