using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.User
{
    public interface IUserService
    {
        IQueryable<Domain.Models.Identity.User> GetAll();
        Task<Domain.Models.Identity.User> CreateAsync(Domain.Models.Identity.User entity);

        Task<Domain.Models.Identity.User> UpdateAsync(Guid entityId, Domain.Models.Identity.User entity);
        Task<Domain.Models.Identity.User> DeleteUserAsync(Guid entityId);
    }
}
