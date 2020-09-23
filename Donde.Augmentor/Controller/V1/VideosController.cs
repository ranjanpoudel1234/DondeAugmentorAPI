using AutoMapper;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V1
{

    [ApiVersion("1.0")]
    [ODataRoutePrefix("videos")]
    [Authorize]
    public class VideosController : BaseController
    {
        private readonly IVideoService _videoService;
        private readonly IMapper _mapper;
        private readonly ILogger<AugmentImagesController> _logger;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly DomainSettings _domainSettings;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public VideosController(IVideoService videoService,
            IMapper mapper, IFileProcessingService fileProcessingService,
            ILogger<AugmentImagesController> logger,
            DomainSettings domainSettings)
        {
            _videoService = videoService;
            _mapper = mapper;
            _logger = logger;
            _fileProcessingService = fileProcessingService;
            _domainSettings = domainSettings;
        }

        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(52428800)] // 50 mb
        public async Task<IActionResult> Upload()
        {
            var organizationId = GetCurrentOrganizationIdOrThrow();

            var fileUploadResult = await _fileProcessingService.UploadMediaAsync(Request, MediaTypes.Video);

            if (fileUploadResult.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            var video = _mapper.Map<Video>(fileUploadResult.Value);
            video.OrganizationId = organizationId;

            var addedVideo = await _videoService.AddVideoAsync(video);

            var addedVideoViewModel = _mapper.Map<VideoViewModel>(addedVideo);

            addedVideoViewModel.Url = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.VideosFolderName,
            addedVideoViewModel.FileId, addedVideoViewModel.Extension);

            return Created(addedVideoViewModel);
        }
    }
}
