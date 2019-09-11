using Donde.Augmentor.Core.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAugmentImageRepository
    {
        IQueryable<AugmentImage> GetAugmentImages();
        Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage);
    }
}
