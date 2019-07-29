using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IGenericRepository
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IDondeModel;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : class, IDondeModel;

        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IDondeModel, IAuditFieldsModel;

        Task<TEntity> UpdateAsync<TEntity>(Guid id, TEntity entity) where TEntity : class, IDondeModel, IAuditFieldsModel;
    }
}
