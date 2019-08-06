using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
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
        private readonly IStorageService _storageService;
        private readonly IHostingEnvironment _hostingEnvironment;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public AugmentImagesController(IAugmentImageService augmentImageservice,
            IMapper mapper, IStorageService storageService, 
            ILogger<AugmentImagesController> logger,
            IHostingEnvironment env)
        {
            _augmentImageservice = augmentImageservice;
            _mapper = mapper;
            _logger = logger;
            _storageService = storageService;
            _hostingEnvironment = env;

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
        [RequestSizeLimit(100000000)] // 100 mb
        public async Task<IActionResult> Upload()
        {
            using (var stream = await new FileStreamingHelper().StreamFile(Request))
            {
                var fileName = Guid.NewGuid().ToString();
                var filePath = CreateFileAndReturnPath(stream, fileName);

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    var uploadResult = await _storageService.UploadFileAsync("bucketofpankaj", fileName, filePath);

                    if (uploadResult)
                        DeleteFileFromPath(filePath);
                }     
            }

            return Ok();
        }


        private string CreateFileAndReturnPath(Stream stream, string fileName)
        {
            var contentRoot = _hostingEnvironment.ContentRootPath;
            var uploadsPath = Path.Combine(contentRoot, "Uploads");
            var directoryExists = Directory.Exists(uploadsPath);

            if(!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var path = Path.Combine(uploadsPath, Path.Combine(fileName));

            try
            {
                // Delete the file if it exists.
                if (System.IO.File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    System.IO.File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = System.IO.File.Create(path))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fs);
                    return path;
                }         
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }

        private bool DeleteFileFromPath(string filePath)
        {
            // Delete the file if it exists.
            if (System.IO.File.Exists(filePath))
            {
                // Note that no lock is put on the
                // file and the possibility exists
                // that another process could do
                // something with it between
                // the calls to Exists and Delete.
                System.IO.File.Delete(filePath);
            }

            return true;
        }
    } 
}
