using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.RoleAndPermission;
using System.Linq;

namespace Donde.Augmentor.Core.Services.Services.RoleAndPermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public IQueryable<Permission> GetAll()
        {
            return _permissionRepository.GetAll();
        }
    }
}
