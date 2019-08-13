using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentImages")]
    public class AugmentImagesController : ODataController
    {
        private readonly IAugmentImageService _augmentImageservice;
        private readonly IMapper _mapper;
        private readonly ILogger<AugmentImagesController> _logger;
        private readonly IFileProcessingService _fileProcessingService;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public AugmentImagesController(IAugmentImageService augmentImageservice,
            IMapper mapper, IFileProcessingService fileProcessingService, 
            ILogger<AugmentImagesController> logger,
            IHostingEnvironment env)
        {
            _augmentImageservice = augmentImageservice;
            _mapper = mapper;
            _logger = logger;
            _fileProcessingService = fileProcessingService;

        }

        [ODataRoute]
        [HttpGet]
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

            return Ok(result);
        }


        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(52428800)] // 50 mb
        public async Task<IActionResult> Upload()
        {
            var result = await _fileProcessingService.UploadImageAsync(Request);

            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            //call augmentImage service with dto and organizationId from header.

            return Ok();
        }
    } 
}
