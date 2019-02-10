using Donde.Augmentor.Core.Domain.Models;
using System.Linq;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAugmentImageRepository
    {
        IQueryable<AugmentImage> GetAugmentImages();
    }
}
