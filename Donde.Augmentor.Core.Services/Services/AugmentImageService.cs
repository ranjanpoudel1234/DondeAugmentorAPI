using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System.Linq;

namespace Donde.Augmentor.Core.Services.Services
{
    public class AugmentImageService : IAugmentImageService
    {
        private IAugmentImageRepository _augmentImageRepository;

        public AugmentImageService(IAugmentImageRepository AugmentImageRepository)
        {
            _augmentImageRepository = AugmentImageRepository;
        }

        public IQueryable<AugmentImage> GetAugmentImages()
        {
            return _augmentImageRepository.GetAugmentImages();
        }
    }
}
