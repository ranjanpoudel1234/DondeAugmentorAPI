using AutoMapper;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentObjects")]
    public class AugmentObjectController : ODataController
    {
        private readonly IAugmentObjectService _augmentObjectService;
        private readonly IMapper _mapper;
        
        public AugmentObjectController(IAugmentObjectService augmentObjectService, IMapper mapper)
        {
            _augmentObjectService = augmentObjectService;
            _mapper = mapper;
        }

        [HttpGet]
        [ODataRoute]
        public async Task<IActionResult> GetAugmentObject([FromODataUri] double latitude, [FromODataUri] double longitude, [FromODataUri] int radiusInMeters)
        {
            var result = await _augmentObjectService.GetClosestAugmentObjectsByRadius(latitude, longitude, radiusInMeters);

            var mappedResult = _mapper.Map<List<AugmentObjectViewModel>>(result);

            return Ok(mappedResult);
        }
    }
}
