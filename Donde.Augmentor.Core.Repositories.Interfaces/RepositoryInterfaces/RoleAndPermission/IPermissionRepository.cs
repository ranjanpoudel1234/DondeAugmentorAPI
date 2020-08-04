using System;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System.Linq;


namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission
{
    public interface IPermissionRepository
    {
        IQueryable<Permission> GetAll();
    }
}
