using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.Targets;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ODataQueryOptions<TargetViewModel> odataOptions)
        {
            var augmentObjectQueryable = _augmentObjectService.GetAugmentObjectsQueryableWithChildren();

            var projectedTargets = augmentObjectQueryable.ProjectTo<TargetViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = (IQueryable<TargetViewModel>)odataOptions.ApplyTo(projectedTargets);

            var result = await appliedResults.ToListAsync();      

          //  MapAvatarConfiguration(result);

            return Ok(result.ToODataCollectionResponse(Request));
        }

        private void MapMediaUrlsAndAvatarConfiguration(List<TargetViewModel> targetViewModels)
        {
            foreach (var target in targetViewModels)
            {
                target.Image.ThumbnailUrl = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, 
                    _domainSettings.UploadSettings.ImageFolderName, target.Image.FileId, target.Image.Extension);

                target.Image.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                 _domainSettings.UploadSettings.ImageFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, target.Image.FileId, target.Image.Extension);

                if (target.Media.Type == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio)
                {
                    target.Avatar.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                     _domainSettings.UploadSettings.AvatarFolderName,
                    target.Avatar.OrganizationId.ToString(),
                    target.Avatar.FileId, target.Avatar.Extension);

                    target.Image.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                _domainSettings.UploadSettings.ImageFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, target.Image.FileId, target.Image.Extension);
                }
                else if(target.Media.Type == Core.Domain.Enum.AugmentObjectMediaTypes.Video)
                {
                    target.Image.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
                _domainSettings.UploadSettings.ImageFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, target.Image.FileId, target.Image.Extension);
                }

            }
        }

        //var organizationQueryable = _roleService.GetAll();

        //var projectedRoles = organizationQueryable.ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider);

        //var appliedResults = (IQueryable<RoleViewModel>)odataOptions.ApplyTo(projectedRoles);

        //var result = await appliedResults.ToListAsync();

        //var result2 = result.Cast<dynamic>();

        //    //without dynamic, the child collection will not be loaded. 
        //    //when using dynamic, the format of response will be just plain array
        //    //to format in Odata style, we call this extension below.
        //    return Ok(result2.ToODataCollectionResponse(Request));
    }
}
