using System;
using System.Threading.Tasks;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;

namespace Donde.Augmentor.Core.Services.Services
{
    public class OrganizationResourceService : IOrganizationResourceService
    {
        private readonly IAudioService _audioService;
        private readonly IVideoService _videoService;
        private readonly IAugmentImageService _augmentImageService;
        private readonly IAvatarService _avatarService;
        private readonly IAugmentObjectService _augmentObjectService;

        public OrganizationResourceService(IAudioService audioService, 
            IVideoService videoService, 
            IAugmentImageService augmentImageService,
            IAvatarService avatarService,
            IAugmentObjectService augmentObjectService)
        {
            _audioService = audioService;
            _videoService = videoService;
            _augmentImageService = augmentImageService;
            _avatarService = avatarService;
            _augmentObjectService = augmentObjectService;
        }

        public async Task DeleteOrganizationResourcesByOrganizationIdAsync(Guid organizationId)
        {
            //@improvement, parallelization with DbContextFactory later.
            await _audioService.DeleteAudiosByOrganizationIdAsync(organizationId);

            await _videoService.DeleteVideosByOrganizationIdAsync(organizationId);

            await _avatarService.DeleteAvatarsByOrganizationIdAsync(organizationId);

            await _augmentImageService.DeleteAugmentImagesByOrganizationIdAsync(organizationId);

            await _augmentObjectService.DeleteAugmentObjectsByOrganizationIdAsync(organizationId);

        }
    }
}
