using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("organizations")]
    [Authorize]
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

        [HttpGet("api/v1/organizationsGeocoded")]
        public async Task<IActionResult> GetOrganizationsGeocoded(double latitude, double longitude, int radiusInMeters)
        {
            //@todo, make this appSettings later.
            //@todo add top by default to odata query.
            radiusInMeters = radiusInMeters == 0 ? 50 * 1610 : radiusInMeters; // 50 mile times 1690 meter per mile.

            //@todo, add validation here for checking latitude and longitude.

            var result = await _organizationService.GetClosestOrganizationByRadius(latitude, longitude, radiusInMeters);

            var mappedResult = _mapper.Map<List<OrganizationViewModel>>(result);

            return Ok(mappedResult);
        }


        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<OrganizationViewModel> odataOptions)
        {
            var result = new List<OrganizationViewModel>();

            var organizationQueryable = _organizationService.GetOrganizations();

            var projectedOrganizations = organizationQueryable.ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedOrganizations);

            var organizationsViewModels = appliedResults as IQueryable<OrganizationViewModel>;

            if (organizationsViewModels != null)
            {
                result = await organizationsViewModels.ToListAsync();
            }

            return Ok(result);
        }

        [ODataRoute]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrganizationViewModel organizationViewModel)
        {
            var organization = _mapper.Map<Organization>(organizationViewModel);

            var result = await _organizationService.CreateOrganizationAsync(organization);

            var organizationViewModelResult = _mapper.Map<OrganizationViewModel>(result);

            return Ok(organizationViewModelResult);
        }
    }
}
