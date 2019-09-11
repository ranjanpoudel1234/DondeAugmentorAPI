using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("audios")]
    public class AudiosController : ODataController
    {
        private readonly IAudioService _audioService;
     //   private readonly IS3Service _s3Service;
        private readonly IMapper _mapper;
        private readonly ILogger<AudiosController> _logger;

        public AudiosController(IAudioService audioService, IMapper mapper, ILogger<AudiosController> logger)
        {
            _audioService = audioService;
            _mapper = mapper;
            _logger = logger;
           // _s3Service = s3Service;
        }

        [ODataRoute]
        [HttpGet]
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

            return Ok(result);
        }


        [ODataRoute]
        [HttpPost("UploadSmallAudio")]
        public async Task<Audio> UploadSmallAudio(IFormFile file)
        {
            string bucketName = "booketofpankaj1";
            string bucketToReadFrom = "bucketofpankaj";
          //  await _s3Service.GetObjectFromS3Async(bucketToReadFrom);
            //var createBucketResponse = await _s3Service.CreateBucketAsync(bucketName);
            //var uploadResponse = await _s3Service.UploadObjectAsync(file, bucketName);

            return null;        
        }
    }
}
