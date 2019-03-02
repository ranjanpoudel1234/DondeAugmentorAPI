using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AudiosController> _logger;

        public AudiosController(IAudioService audioService, IMapper mapper, ILogger<AudiosController> logger)
        {
            _audioService = audioService;
            _mapper = mapper;
            _logger = logger;
        }

        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<AudioViewModel> odataOptions)
        {
            try
            {

                _logger.LogTrace("Audios Controller Trace");
                _logger.LogDebug("Audios Controller Debug");
                _logger.LogInformation("Audios Controller Information");
                _logger.LogWarning("Audios Controller Warning");
                _logger.LogError("Audios Controller Error");
                _logger.LogCritical("Audios Controller Fatal");
                var result = new List<AudioViewModel>();
                var audiosQueryable = _audioService.GetAudios();

                var projectedAudios = audiosQueryable.ProjectTo<AudioViewModel>(_mapper.ConfigurationProvider);

                var appliedResults = odataOptions.ApplyTo(projectedAudios);

                // ToListAsync converts Iqueryable<T> to List<T>. thus cast needed
                var audioViewModels = appliedResults as IQueryable<AudioViewModel>;

                if (audioViewModels != null)
                {
                    result = await audioViewModels.ToListAsync();
                }
                _logger.LogInformation("Audios Controller Exit");
                return Ok(result);
            }
            catch(Exception ex)
            {
                var test = ex;
            }
            return null;
        }
    }
}
