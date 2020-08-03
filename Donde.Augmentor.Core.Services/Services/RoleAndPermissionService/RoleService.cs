using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.RoleAndPermission;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.RoleAndPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Donde.Augmentor.Core.Services.Services.RoleAndPermissionService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }
    }
}
