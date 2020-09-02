using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.Targets;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Donde.Augmentor.Web.Attributes.IgnoreJsonIgnore;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.TargetsRoute)]
    [Authorize]
    public class TargetsController : BaseController
    {
        private readonly IAugmentObjectService _augmentObjectService;
        private readonly IMapper _mapper;
        private readonly DomainSettings _domainSettings;
        public TargetsController(IAugmentObjectService augmentObjectService, IMapper mapper, DomainSettings domainSettings)
        {
            _augmentObjectService = augmentObjectService;
            _mapper = mapper;
            _domainSettings = domainSettings;
        }

        /// <summary>
        /// @PotentialPerformanceIfNeeded: If the get shows any sign of slowness, another POC could be 
        /// to  load augmentObjects first with mediaType and Image, apply odata,
        /// then call audio and video based on the mediatype and load those properties afterwards
        /// </summary>
        /// <param name="odataOptions"></param>
        /// <returns></returns>
        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ODataQueryOptions<TargetViewModel> odataOptions)
        {
            var augmentObjectQueryable = _augmentObjectService.GetAugmentObjectsQueryableWithChildren();

            var projectedTargets = augmentObjectQueryable.ProjectTo<TargetViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = (IQueryable<TargetViewModel>)odataOptions.ApplyTo(projectedTargets);

            var result = await appliedResults.ToListAsync();

            foreach (var target in result)
            {
                MapMediaAndAvatar(target);
            }

            return Ok(result.Cast<dynamic>().ToODataCollectionResponse(Request));
        }

        [ODataRoute]
        [HttpPost]
        [IgnoreJsonIgnore]
        public async Task<IActionResult> Post([FromBody] TargetViewModel targetViewModel)
        {
            var target = _mapper.Map<AugmentObject>(targetViewModel);

            var media = _mapper.Map<AugmentObjectMedia>(targetViewModel.Media);       

            if(targetViewModel.Media.Type == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio)
            {
                media.AvatarId = targetViewModel.Avatar.Id;
            }

            target.AugmentObjectMedias.Add(media);

            var locations = _mapper.Map<List<AugmentObjectLocation>>(targetViewModel.Locations);
            if (locations != null)
            {
                target.AugmentObjectLocations.AddRange(locations);
            }

            var result = await _augmentObjectService.CreateAugmentObjectAsync(target);
            var addedTargetViewModel = _mapper.Map<TargetViewModel>(result);

            MapMediaAndAvatar(addedTargetViewModel);

            return Ok(addedTargetViewModel);
        }

        private void MapMediaAndAvatar(TargetViewModel target)
        {

            target.Image.ThumbnailUrl = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath,
                                                        _domainSettings.UploadSettings.ImagesFolderName,
                                                        target.Image.FileId,
                                                        target.Image.Extension);

            target.Image.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                                                         _domainSettings.UploadSettings.ImagesFolderName,
                                                         _domainSettings.UploadSettings.OriginalImageSubFolderName,
                                                         target.Image.FileId,
                                                         target.Image.Extension);

            if (target.Media.Type == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio)
            {
                var avatar = target.Avatar;
                avatar.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                                                              _domainSettings.UploadSettings.AvatarsFolderName,
                                                              avatar.OrganizationId.ToString(),
                                                              avatar.FileId,
                                                              avatar.Extension);

                avatar.Configuration = !string.IsNullOrWhiteSpace(avatar.ConfigurationString)
                 ? JsonConvert.DeserializeObject<AvatarConfigurationViewModel>(avatar.ConfigurationString) : null;


                var media = target.Media;
                media.Url = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath,
                                                            _domainSettings.UploadSettings.AudiosFolderName,
                                                            media.FileId,
                                                            media.Extension);

            }
            else if (target.Media.Type == Core.Domain.Enum.AugmentObjectMediaTypes.Video)
            {
                target.Avatar = null; //setting null here so we dont have object with default propertyvalues(from Automapper)
                var media = target.Media;
                media.Url = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath,
                                                            _domainSettings.UploadSettings.VideosFolderName,
                                                            media.FileId,
                                                            media.Extension);
            }
        }
    }
}
