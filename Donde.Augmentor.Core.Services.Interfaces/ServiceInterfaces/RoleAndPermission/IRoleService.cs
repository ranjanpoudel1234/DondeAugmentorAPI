using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System.Linq;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.RoleAndPermission
{
    public interface IRoleService
    {
        IQueryable<Role> GetAll();
    }
}
