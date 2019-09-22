using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters)
        {
            return await _organizationRepository.GetClosestOrganizationByRadius(latitude, longitude, radiusInMeters);
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return _organizationRepository.GetOrganizations();
        }

        public async Task<Organization> CreateOrganizationAsync(Organization entity)
        {
            //todo need to add fluent validation here.
            return await _organizationRepository.CreateOrganizationAsync(entity);
        }
    }
}
