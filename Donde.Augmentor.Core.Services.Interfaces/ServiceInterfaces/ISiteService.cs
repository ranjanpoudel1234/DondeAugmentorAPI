using Donde.Augmentor.Core.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface ISiteService
    {
        IQueryable<Site> GetSites();
        Task<Site> CreateSiteAsync(Site entity);
    }
}
