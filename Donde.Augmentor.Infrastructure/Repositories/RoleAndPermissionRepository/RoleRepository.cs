using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Donde.Augmentor.Infrastructure.Repositories.RoleAndPermissionRepository
{
    public class RoleRepository : GenericRepository, IRoleRepository
    {
        public RoleRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Role> GetAll()
        {
            return GetAll<Role>().Include(role => role.Permissions).ThenInclude(rolePermission => rolePermission.Permission);
        }

        public IQueryable<Role> GetRoleByIds(List<Guid> roleIds)
        {
            return GetAll<Role>().Where(x => roleIds.Contains(x.Id));
        }
    }
}
