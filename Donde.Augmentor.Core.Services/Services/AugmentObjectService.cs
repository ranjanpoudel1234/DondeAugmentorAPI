using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
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

        public AugmentObjectService(IAugmentObjectRepository augmentObjectRepository, DomainSettings domainSettings)
        {
            _augmentObjectRepository = augmentObjectRepository;
            _domainSettings = domainSettings;
        }

        public IQueryable<AugmentObjectDto> GetAugmentObjects()
        {
           var augmentObjects = _augmentObjectRepository.GetAugmentObjects().Select(augmentObject => GetAugmentObjectMapWithUpdatedUrls(augmentObject));

            return augmentObjects;
        }

        public async Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity)
        {
            //todo need to add fluent validation here.
            return await _augmentObjectRepository.CreateAugmentObjectAsync(entity);
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

        private string GetPathWithRootLocationOrNull(string url, bool getOriginal = false)
        {
            if (url == null) return null;

            if (!getOriginal)
            return $"{_domainSettings.GeneralSettings.StorageBasePath}{url}";

            var urlSplit = url.Split("/");
            return $"{_domainSettings.GeneralSettings.StorageBasePath}{urlSplit[0]}/{_domainSettings.UploadSettings.OriginalImageSubFolderName}/{urlSplit[1]}";
        }

        private string GetThumbnailMediaPath(string folderName, Guid fileId, string extension)
        {
             return $"{_domainSettings.GeneralSettings.StorageBasePath}{folderName}/{fileId}{extension}";
        }

        private string GetOriginalMediaPath(string folderName, string originalMediaFolderName, Guid fileId, string extension)
        {
            return $"{_domainSettings.GeneralSettings.StorageBasePath}{folderName}/{originalMediaFolderName}/{fileId}{extension}";
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
                AvatarUrl = GetPathWithRootLocationOrNull(augmentObject.AvatarUrl),
                AvatarConfiguration = augmentObject.AvatarConfiguration,
                AudioId = augmentObject.AudioId,
                AudioName = augmentObject.AudioName == null ? null : augmentObject.AudioName,
                AudioUrl = GetPathWithRootLocationOrNull(augmentObject.AudioUrl),
                VideoId = augmentObject.VideoId,
                VideoName = augmentObject.VideoName == null ? null : augmentObject.VideoName,
                VideoUrl = GetPathWithRootLocationOrNull(augmentObject.VideoUrl),
                ImageName = augmentObject.ImageName,
                ImageUrl = GetThumbnailMediaPath(_domainSettings.UploadSettings.ImageFolderName, augmentObject.ImageFileId, augmentObject.ImageFileExtension),
                OriginalSizeImageUrl = GetOriginalMediaPath(_domainSettings.UploadSettings.ImageFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, augmentObject.ImageFileId, augmentObject.ImageFileExtension),
                Distance = augmentObject.Distance,
                Latitude = augmentObject.Latitude,
                Longitude = augmentObject.Longitude
            };
        }
    }
}
