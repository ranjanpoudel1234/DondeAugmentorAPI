using Donde.Augmentor.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters);
    }
}
