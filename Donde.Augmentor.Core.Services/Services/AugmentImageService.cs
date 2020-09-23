using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage)
        {
            return _augmentImageRepository.AddAugmentImageAsync(augmentImage);
        }

        public Task<AugmentImage> GetAugmentImageByIdAsync(Guid augmentImageId)
        {
            return _augmentImageRepository.GetAugmentImageByIdAsync(augmentImageId);
        }

        public async Task DeleteAugmentImagesByOrganizationIdAsync(Guid organizationId)
        {
            var augmentImagesByOrganization = await _augmentImageRepository.GetAugmentImagesByOrganizationIdAsync(organizationId);
            foreach (var augmentImage in augmentImagesByOrganization)
            {
                augmentImage.IsDeleted = true;
                await _augmentImageRepository.UpdateAugmentImageAsync(augmentImage.Id, augmentImage);
            }
        }
    }
}
