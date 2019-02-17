using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System.Linq;

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
    }
}
