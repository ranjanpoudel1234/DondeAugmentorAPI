using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetAll();
        IQueryable<Role> GetRoleByIds(List<Guid> roleIds);
    }
}
