using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.RoleAndPermission
{
    public interface IRoleService
    {
        IQueryable<Role> GetAll();
        IQueryable<Role> GetRoleByIds(List<Guid> roleIds);
    }
}
