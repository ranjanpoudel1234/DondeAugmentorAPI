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


namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("audios")]
    public class AudiosController : ODataController
    {
        private readonly IAudioService _audioService;
        private readonly IMapper _mapper;

        public AudiosController(IAudioService audioService, IMapper mapper)
        {
            _audioService = audioService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<AudioViewModel> odataOptions)
        {
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

            return Ok(result);
        }
    }
}
