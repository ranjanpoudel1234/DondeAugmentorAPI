using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Threading.Tasks;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Services.Services.CustomValidations
{
    public class AugmentObjectResourceValidator : IAugmentObjectResourceValidator
    {
        private readonly IAudioService _audioService;
        private readonly IVideoService _videoService;
        private readonly IAvatarService _avatarService;
        private readonly IAugmentImageService _augmentImageService;

        public AugmentObjectResourceValidator(IAudioService audioService, IVideoService videoService, 
            IAvatarService avatarService, IAugmentImageService augmentImageService)
        {
            _audioService = audioService;
            _videoService = videoService;
            _avatarService = avatarService;
            _augmentImageService = augmentImageService;
        }

        public async Task ValidateAugmentObjectResourceOrThrowAsync(AugmentObject entity)
        {
            var augmentImage = await _augmentImageService.GetAugmentImageByIdAsync(entity.AugmentImageId);
            if(augmentImage == null)
                throw new HttpNotFoundException($"{entity.AugmentImageId} | {ErrorMessages.ObjectNotFound}");
            if (augmentImage.OrganizationId != entity.OrganizationId)
                throw new HttpBadRequestException($"{entity.AugmentImageId} | {DondeErrorMessages.RESOURCE_DOES_NOT_BELONG_TO_ORGANIZATION}");


            foreach(var media in entity.AugmentObjectMedias)
            {
                if (media.MediaType == Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio)
                {
                    var avatar = await _avatarService.GetAvatarByIdAsync(media.AvatarId.Value);
                    if (avatar == null)
                        throw new HttpNotFoundException($"{media.AvatarId.Value} | {ErrorMessages.ObjectNotFound}");
                    if (avatar.OrganizationId != entity.OrganizationId)
                        throw new HttpBadRequestException($"{media.AvatarId.Value} | {DondeErrorMessages.RESOURCE_DOES_NOT_BELONG_TO_ORGANIZATION}");

                    var audio = await _audioService.GetAudioByIdAsync(media.AudioId.Value);
                    if (audio == null)
                        throw new HttpNotFoundException($"{media.AudioId.Value} | {ErrorMessages.ObjectNotFound}");
                    if (avatar.OrganizationId != entity.OrganizationId)
                        throw new HttpBadRequestException($"{media.AudioId.Value} | {DondeErrorMessages.RESOURCE_DOES_NOT_BELONG_TO_ORGANIZATION}");
                }
                else if (media.MediaType == Domain.Enum.AugmentObjectMediaTypes.Video)
                {

                    var video = await _videoService.GetVideoByIdAsync(media.VideoId.Value);
                    if (video == null)
                        throw new HttpNotFoundException($"{media.VideoId.Value} | {ErrorMessages.ObjectNotFound}");
                    if (video.OrganizationId != entity.OrganizationId)
                        throw new HttpBadRequestException($"{media.VideoId.Value} | {DondeErrorMessages.RESOURCE_DOES_NOT_BELONG_TO_ORGANIZATION}");
                }
            }      
        }
    }
}
