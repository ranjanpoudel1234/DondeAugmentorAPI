using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.ViewModels.V1;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V1
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentImages")]
    [Authorize]
    public class AugmentImagesController : BaseController
    {
        private readonly IAugmentImageService _augmentImageservice;
        private readonly IMapper _mapper;
        private readonly ILogger<AugmentImagesController> _logger;
        private readonly IFileProcessingService _fileProcessingService;
        public readonly DomainSettings _domainSettings;

        public AugmentImagesController(IAugmentImageService augmentImageservice,
            IMapper mapper, IFileProcessingService fileProcessingService, 
            ILoggerFactory loggerFactory,
            DomainSettings domainSettings)
        {
            _augmentImageservice = augmentImageservice;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<AugmentImagesController>();
            _fileProcessingService = fileProcessingService;
            _domainSettings = domainSettings;

        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ODataQueryOptions<AugmentImageViewModel> odataOptions)
        {
            var result = new List<AugmentImageViewModel>();

            var augmentImagesQueryable = _augmentImageservice.GetAugmentImages();

            var projectedAugmentImages = augmentImagesQueryable.ProjectTo<AugmentImageViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedAugmentImages);

            var augmentImageViewModels = appliedResults as IQueryable<AugmentImageViewModel>;

            if (augmentImageViewModels != null)
            {
                result = await augmentImageViewModels.ToListAsync();
            }

            foreach (var augmentImage in result)
            {
                augmentImage.ThumbnailUrl = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.ImagesFolderName, augmentImage.FileId, augmentImage.Extension);
                augmentImage.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.ImagesFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, augmentImage.FileId, augmentImage.Extension);
            }
      
            return Ok(result);
        }


        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(15728640)] // 15 mb
        public async Task<IActionResult> Upload()
        {
            var organizationId = GetCurrentOrganizationIdOrThrow();
          
            var fileUploadResult = await _fileProcessingService.UploadMediaAsync(Request, MediaTypes.Image);
         
            if (fileUploadResult.IsFailure)
            {
                _logger.LogError($"Error on file Upload {JsonConvert.SerializeObject(fileUploadResult)}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
            var augmentImage = _mapper.Map<AugmentImage>(fileUploadResult.Value);
            augmentImage.OrganizationId = organizationId;

            var addedAugmentImage = await _augmentImageservice.AddAugmentImageAsync(augmentImage);

            var augmentImageViewModel = _mapper.Map<AugmentImageViewModel>(addedAugmentImage);

            augmentImageViewModel.ThumbnailUrl = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.ImagesFolderName, augmentImageViewModel.FileId, augmentImageViewModel.Extension);
            augmentImageViewModel.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.ImagesFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, augmentImageViewModel.FileId, augmentImageViewModel.Extension);

            return Created(augmentImageViewModel);
        }
    } 
}
