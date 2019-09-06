using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentObjects")]
    public class AugmentObjectsController : ODataController
    {
        private readonly IAugmentObjectService _augmentObjectService;
        private readonly IMapper _mapper;
        
        public AugmentObjectsController(IAugmentObjectService augmentObjectService, IMapper mapper)
        {
            _augmentObjectService = augmentObjectService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<AugmentObjectViewModel> odataOptions)
        {
            var result = new List<AugmentObjectViewModel>();

            var augmentObjectQueryable = _augmentObjectService.GetAugmentObjects();

            var projectedAudios = augmentObjectQueryable.ProjectTo<AugmentObjectViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedAudios);

            var augmentObjectViewModels = appliedResults as IQueryable<AugmentObjectViewModel>;

            if (augmentObjectViewModels != null)
            {
                result = await augmentObjectViewModels.ToListAsync();
            }

            return Ok(result);
        }

        [HttpGet("api/v1/augmentObjectsGeocoded")]
       
        public async Task<IActionResult> GetAugmentObject(double latitude, double longitude, int radiusInMeters)
        {
            var result = await _augmentObjectService.GetClosestAugmentObjectsByRadius(latitude, longitude, radiusInMeters);

            var mappedResult = _mapper.Map<List<AugmentObjectViewModel>>(result);

            return Ok(mappedResult);
        }

        [ODataRoute]
        [HttpPost]
        public async Task<IActionResult> Post(AugmentObjectViewModel augmentObjectViewModel)
        {
            var augmentObject = _mapper.Map<AugmentObject>(augmentObjectViewModel);

            var result = await _augmentObjectService.CreateAugmentObjectAsync(augmentObject);

            var addedAugmentObjectViewModel = _mapper.Map<AugmentObjectViewModel>(result);

            return Ok(addedAugmentObjectViewModel);
        }
    }
}
