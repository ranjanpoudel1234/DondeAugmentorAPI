using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.User
{
    public interface IUserRepository
    {
        IQueryable<Domain.Models.Identity.User> GetAll();
        Task<Domain.Models.Identity.User> GetByIdAsync(Guid entityId);
        Task<Domain.Models.Identity.User> GetByIdWithNoTrackingAsync(Guid entityId);
        Task<Domain.Models.Identity.User> CreateAsync(Domain.Models.Identity.User entity);
        Task UpdateUserOrganizationsAsync(Domain.Models.Identity.User entity);
        Task<Domain.Models.Identity.User> DeleteAsync(Domain.Models.Identity.User entity);
    }
}
