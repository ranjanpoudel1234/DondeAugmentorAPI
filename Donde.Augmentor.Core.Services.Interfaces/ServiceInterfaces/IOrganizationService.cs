using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters);
        IQueryable<Organization> GetOrganizations();
        Task<Organization> GetOrganizationByIdAsync(Guid organizationId);
        Task<Organization> CreateOrganizationAsync(Organization entity);
        Task<Organization> UpdateOrganizationAsync(Guid entityId, Organization entity);
    }
}
