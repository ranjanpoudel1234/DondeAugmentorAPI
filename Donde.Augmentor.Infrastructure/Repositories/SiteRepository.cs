using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class SiteRepository : GenericRepository, ISiteRepository
    {
        public SiteRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public async Task<Site> CreateSiteAsync(Site entity)
        {
            return await CreateAsync(entity);
        }

        public IQueryable<Site> GetSites()
        {
            return GetAllAsNoTracking<Site>();
        }
    }
}
