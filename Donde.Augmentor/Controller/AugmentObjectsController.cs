using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentObjects")]
    [Authorize]
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
        [AllowAnonymous]
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

        [HttpGet("api/v1/organizations/{organizationId}/geographicalAugmentObjects")]
        [AllowAnonymous]
       
        public async Task<IActionResult> GetAugmentObjectGeocoded(Guid organizationId, double latitude, double longitude, int radiusInMeters)
        {
            if(latitude == 0 || longitude == 0)
            {
                throw new HttpBadRequestException("Latitude and Longitude query string required");
            }

            if (radiusInMeters == 0)
                radiusInMeters = 500;// hardcoded for now.

            var result = await _augmentObjectService.GetGeographicalAugmentObjectsByRadius(organizationId, latitude, longitude, radiusInMeters);

            var mappedResult = _mapper.Map<List<GeographicalAugmentObjectsViewModel>>(result);

            return Ok(mappedResult);
        }

        [ODataRoute]
        [HttpPost]
        ///Improvement, there could be two different post endpoint, one geographical, one regular returning 
        ///respective viewModels that match their GET counterparts.
        public async Task<IActionResult> Post([FromBody] AugmentObjectPostViewModel augmentObjectPostViewModel)
        {
            var augmentObject = _mapper.Map<AugmentObject>(augmentObjectPostViewModel);

            var augmentObjectId = augmentObject.Id;
            var augmentObjectMedia = _mapper.Map<AugmentObjectMedia>(augmentObjectPostViewModel.AugmentObjectMedia);
            augmentObjectMedia.AugmentObjectId = augmentObjectId;
            augmentObject.AugmentObjectMedias.Add(augmentObjectMedia);

            var augmentObjectLocations = _mapper.Map<List<AugmentObjectLocation>>(augmentObjectPostViewModel.AugmentObjectLocations);
            if(augmentObjectLocations != null)
            {
                augmentObjectLocations.ForEach(x => x.AugmentObjectId = augmentObjectId);
                augmentObject.AugmentObjectLocations.AddRange(augmentObjectLocations);
            }
           
            var result = await _augmentObjectService.CreateAugmentObjectAsync(augmentObject);
            var addedAugmentObjectViewModel = _mapper.Map<GeographicalAugmentObjectsViewModel>(result);

            return Ok(addedAugmentObjectViewModel);
        }
    }
}
