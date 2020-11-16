using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IOrganizationResourceService
    {
        Task DeleteOrganizationResourcesByOrganizationIdAsync(Guid oranizationId);
    }
}
