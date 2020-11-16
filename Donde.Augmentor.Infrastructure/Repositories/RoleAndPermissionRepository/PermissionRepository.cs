using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission;
using Donde.Augmentor.Infrastructure.Database;
using System.Linq;

namespace Donde.Augmentor.Infrastructure.Repositories.RoleAndPermissionRepository
{
    public class PermissionRepository : GenericRepository, IPermissionRepository
    {
        public PermissionRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Permission> GetAll()
        {
            return GetAllAsNoTracking<Permission>();
        }
    }
}
