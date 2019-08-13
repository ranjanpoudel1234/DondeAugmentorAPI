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

        public Task<AugmentImage> AddAugmentImageAsync(MediaAttachmentDto attachmentDto, Guid organizationId)
        {
            //convert dto to image here
            var augmentImage = new AugmentImage();

            return _augmentImageRepository.AddAugmentImageAsync(augmentImage);
        }
    }
}
