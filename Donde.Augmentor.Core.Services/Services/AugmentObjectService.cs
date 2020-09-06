using AutoMapper;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.CustomValidations;
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
        private readonly IMapper _mapper;
        private readonly IAugmentObjectResourceValidationService _augmentObjectResourceValidationService;

        public AugmentObjectService(IAugmentObjectRepository augmentObjectRepository,
            DomainSettings domainSettings, IValidator<AugmentObject> validator,
            IAugmentObjectResourceValidationService augmentObjectResourceValidationService,
             IMapper mapper)
        {
            _augmentObjectRepository = augmentObjectRepository;
            _domainSettings = domainSettings;
            _validator = validator;
            _augmentObjectResourceValidationService = augmentObjectResourceValidationService;
            _mapper = mapper;
        }

        public IQueryable<AugmentObject> GetAugmentObjectsQueryableWithChildren()
        {
            return _augmentObjectRepository.GetAugmentObjectsQueryableWithChildren();
        }

        public Task<AugmentObject> GetAugmentObjectByIdWithChildrenAsync(Guid augmentObjectId)
        {
            return _augmentObjectRepository.GetAugmentObjectByIdWithChildrenAsync(augmentObjectId);
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

            await _augmentObjectResourceValidationService.ValidateAugmentObjectResourceOrThrowAsync(entity);

            await _augmentObjectRepository.CreateAugmentObjectAsync(entity);

            return await _augmentObjectRepository.GetAugmentObjectByIdWithChildrenAsync(entity.Id);
        }

        public async Task<AugmentObject> UpdateAugmentObjectAsync(Guid entityId, AugmentObject entity)
        {
            var existingAugmentObject = await GetAugmentObjectByIdWithChildrenAsync(entityId);

            if (existingAugmentObject == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var mappedAugmentObject = _mapper.Map(entity, existingAugmentObject);
            //deleting existing ones, readding new ones, hence id maps.
            mappedAugmentObject.AugmentObjectMedias.ForEach(x => { x.Id = SequentialGuidGenerator.GenerateComb(); x.AugmentObjectId = mappedAugmentObject.Id; });
            mappedAugmentObject.AugmentObjectLocations.ForEach(x => { x.Id = SequentialGuidGenerator.GenerateComb(); x.AugmentObjectId = mappedAugmentObject.Id; });

            await _validator.ValidateOrThrowAsync(mappedAugmentObject);

            await _augmentObjectResourceValidationService.ValidateAugmentObjectResourceOrThrowAsync(mappedAugmentObject);

            await _augmentObjectRepository.UpdateAugmentObjectAsync(mappedAugmentObject.Id, mappedAugmentObject);

            return await _augmentObjectRepository.GetAugmentObjectByIdWithChildrenAsync(mappedAugmentObject.Id);
        }

        public async Task<IEnumerable<AugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters)
        {
            var geographicalAugmentObjects = await _augmentObjectRepository.GetGeographicalAugmentObjectsByRadius(organizationId, latitude, longitude, radiusInMeters);

            var augmentObjects = geographicalAugmentObjects.Select(augmentObject => GetAugmentObjectMapWithUpdatedUrls(augmentObject));

            return augmentObjects;
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
