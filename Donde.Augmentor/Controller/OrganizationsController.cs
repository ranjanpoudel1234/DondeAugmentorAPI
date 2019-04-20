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
    [ODataRoutePrefix("organizations")]
    public class OrganizationsController : ODataController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationsController> _logger;

        public OrganizationsController(IOrganizationService organizationService, IMapper mapper, ILogger<OrganizationsController> logger)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ODataRoute]
        public async Task<IActionResult> GetAugmentObject(double latitude, double longitude, int radiusInMeters)
        {
            //@todo, make this appSettings later.
            //@todo add top by default to odata query.
            radiusInMeters = radiusInMeters == 0 ? 50 * 1610 : radiusInMeters; // 50 mile times 1690 meter per mile.

            //@todo, add validation here for checking latitude and longitude.

            var result = await _organizationService.GetClosestOrganizationByRadius(latitude, longitude, radiusInMeters);

            var mappedResult = _mapper.Map<List<OrganizationViewModel>>(result);

            return Ok(mappedResult);
        }
    }
}
