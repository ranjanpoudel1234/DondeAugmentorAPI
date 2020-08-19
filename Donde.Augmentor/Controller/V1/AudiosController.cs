using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V1
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("audios")]
    [Authorize]
    public class AudiosController : BaseController
    {
        private readonly IAudioService _audioService;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IMapper _mapper;
        private readonly ILogger<AudiosController> _logger;
        public readonly DomainSettings _domainSettings;

        public AudiosController(IAudioService audioService, 
            IMapper mapper,
            ILogger<AudiosController> logger,
            IFileProcessingService fileProcessingService,
            DomainSettings domainSettings)
        {
            _audioService = audioService;
            _mapper = mapper;
            _logger = logger;
            _fileProcessingService = fileProcessingService;
            _domainSettings = domainSettings;
        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ODataQueryOptions<AudioViewModel> odataOptions)
        {
            var result = new List<AudioViewModel>();

            var audiosQueryable = _audioService.GetAudios();

            var projectedAudios = audiosQueryable.ProjectTo<AudioViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedAudios);

            var audioViewModels = appliedResults as IQueryable<AudioViewModel>;

            if (audioViewModels != null)
            {
                result = await audioViewModels.ToListAsync();
            }

            foreach (var audio in result)
            {
                audio.Url = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.AudiosFolderName,
                    audio.FileId, audio.Extension);
            }

            return Ok(result);
        }

        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(5242880)] // 5 mb
        public async Task<IActionResult> Upload()
        {
            var organizationId = GetCurrentOrganizationIdOrThrow();

            var fileUploadResult = await _fileProcessingService.UploadMediaAsync(Request, MediaTypes.Audio);

            if (fileUploadResult.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            var audio = _mapper.Map<Audio>(fileUploadResult.Value);
            audio.OrganizationId = organizationId;

            var addedVideo = await _audioService.AddAudioAsync(audio);

            var addedVideoViewModel = _mapper.Map<VideoViewModel>(addedVideo);

            addedVideoViewModel.Url = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath, _domainSettings.UploadSettings.AudiosFolderName,
                addedVideoViewModel.FileId, addedVideoViewModel.Extension);

            return Created(addedVideoViewModel);
        }
    }
}
