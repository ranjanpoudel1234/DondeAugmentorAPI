using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Services.Services.CustomValidations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class AugmentObjectService : IAugmentObjectService
    {
        private IAugmentObjectRepository _augmentObjectRepository;
        private DomainSettings _domainSettings;
        private readonly IValidator<AugmentObject> _validator;
        private readonly IAugmentObjectResourceValidator _augmentObjectResourceValidator;

        public AugmentObjectService(IAugmentObjectRepository augmentObjectRepository,
            DomainSettings domainSettings, IValidator<AugmentObject> validator, IAugmentObjectResourceValidator augmentObjectResourceValidator)
        {
            _augmentObjectRepository = augmentObjectRepository;
            _domainSettings = domainSettings;
            _validator = validator;
            _augmentObjectResourceValidator = augmentObjectResourceValidator;
        }

        public IQueryable<AugmentObject> GetAugmentObjectsQueryableWithChildren()
        {
            return _augmentObjectRepository.GetAugmentObjectsQueryableWithChildren();
        }

        public IQueryable<AugmentObjectDto> GetAugmentObjects()
        {
           var augmentObjects = _augmentObjectRepository.GetAugmentObjects().Select(augmentObject => GetAugmentObjectMapWithUpdatedUrls(augmentObject));

            return augmentObjects;
        }

        public async Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity)
        {
            entity.Id = SequentialGuidGenerator.GenerateComb();

            entity.AugmentObjectMedias.ForEach(x => { x.Id = SequentialGuidGenerator.GenerateComb(); x.AugmentObjectId = entity.Id; });

            entity.AugmentObjectLocations.ForEach(x => { x.Id = SequentialGuidGenerator.GenerateComb(); x.AugmentObjectId = entity.Id; });

            await _validator.ValidateOrThrowAsync(entity);

            await _augmentObjectResourceValidator.ValidateAugmentObjectResourceOrThrowAsync(entity);

            await _augmentObjectRepository.CreateAugmentObjectAsync(entity);

            return _augmentObjectRepository.GetAugmentObjectByIdWithChildren(entity.Id);
        }

        private async Task ValidateResourceBelongsToOrganization(Guid organizationId, AugmentObject entity)
        {

        }

        public async Task<IEnumerable<AugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters)
        {
            var geographicalAugmentObjects = await _augmentObjectRepository.GetGeographicalAugmentObjectsByRadius(organizationId, latitude, longitude, radiusInMeters);

            var augmentObjects = geographicalAugmentObjects.Select(augmentObject => GetAugmentObjectMapWithUpdatedUrls(augmentObject));

            return augmentObjects;
        }

        public async Task<AugmentObject> UpdateAugmentObjectAsync(Guid id, AugmentObject entity)
        {
            return await _augmentObjectRepository.UpdateAugmentObjectAsync(id, entity);
        }


        private string GetMediaPath(string folderName, Guid? fileId, string extension)
        {
             return $"{_domainSettings.GeneralSettings.StorageBasePath}{folderName}/{fileId}{extension}";
        }

        private string GetMediaPathWithSubFolder(string folderName, string mediaSubFolderName, Guid? fileId, string extension)
        {
            return $"{_domainSettings.GeneralSettings.StorageBasePath}{folderName}/{mediaSubFolderName}/{fileId}{extension}";
        }

        private AugmentObjectDto GetAugmentObjectMapWithUpdatedUrls(AugmentObjectDto augmentObject)
        {
            return new AugmentObjectDto
            {
                Id = augmentObject.Id,
                AugmentImageId = augmentObject.AugmentImageId,
                Title = augmentObject.Title,
                Description = augmentObject.Description,
                OrganizationId = augmentObject.OrganizationId,
                AddedDate = augmentObject.AddedDate,
                UpdatedDate = augmentObject.UpdatedDate,
                IsDeleted = augmentObject.IsDeleted,
                Type = augmentObject.Type,
                MediaType = augmentObject.MediaType,
                MediaId = augmentObject.MediaId,
                AvatarId = augmentObject.AvatarId,
                AvatarName = augmentObject.AvatarName == null ? null : augmentObject.AvatarName,
                AvatarUrl = augmentObject.AvatarFileId.HasValue ? GetMediaPathWithSubFolder(_domainSettings.UploadSettings.AvatarsFolderName, augmentObject.OrganizationId.ToString(), augmentObject.AvatarFileId, augmentObject.AvatarFileExtension) : null,
                AvatarConfiguration = augmentObject.AvatarConfiguration,
                AudioId = augmentObject.AudioId,
                AudioName = augmentObject.AudioName == null ? null : augmentObject.AudioName,
                AudioUrl = augmentObject.AudioFileId.HasValue ?  GetMediaPath(_domainSettings.UploadSettings.AudiosFolderName, augmentObject.AudioFileId, augmentObject.AudioFileExtension) : null,
                VideoId = augmentObject.VideoId,
                VideoName = augmentObject.VideoName == null ? null : augmentObject.VideoName,
                VideoUrl = augmentObject.VideoFileId.HasValue ?  GetMediaPath(_domainSettings.UploadSettings.VideosFolderName, augmentObject.VideoFileId, augmentObject.VideoFileExtension) : null,
                ImageName = augmentObject.ImageName,
                ImageUrl = GetMediaPath(_domainSettings.UploadSettings.ImagesFolderName, augmentObject.ImageFileId, augmentObject.ImageFileExtension),
                OriginalSizeImageUrl = GetMediaPathWithSubFolder(_domainSettings.UploadSettings.ImagesFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, augmentObject.ImageFileId, augmentObject.ImageFileExtension),
                Distance = augmentObject.Distance,
                Latitude = augmentObject.Latitude,
                Longitude = augmentObject.Longitude
            };
        }
    }
}
