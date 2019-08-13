using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AugmentImageRepository : GenericRepository, IAugmentImageRepository
    {
        public AugmentImageRepository(DondeContext dbContext) : base(dbContext)
        {

        }
    
        public IQueryable<AugmentImage> GetAugmentImages()
        {
            return GetAll<AugmentImage>();
        }

        public async Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage)
        {
            return await CreateAsync(augmentImage);
        }
    }
}
