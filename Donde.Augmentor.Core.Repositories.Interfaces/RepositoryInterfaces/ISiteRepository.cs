using Donde.Augmentor.Core.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface ISiteRepository
    {
        IQueryable<Site> GetSites();
        Task<Site> CreateSiteAsync(Site entity);
    }
}
