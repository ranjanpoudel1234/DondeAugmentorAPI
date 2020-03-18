using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("organizations")]
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationsController> _logger;
        private readonly IFileProcessingService _fileProcessingService;

        public OrganizationsController(IOrganizationService organizationService, 
            IMapper mapper, 
            ILogger<OrganizationsController> logger,
            IFileProcessingService fileProcessingService)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _logger = logger;
            _fileProcessingService = fileProcessingService;
        }

        [HttpGet("api/v1/organizationsGeocoded")] //not in use right now
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        //[ODataRoute]
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] OrganizationViewModel organizationViewModel)
        //{
        //    var organization = _mapper.Map<Organization>(organizationViewModel);

        //    var result = await _organizationService.CreateOrganizationAsync(organization);

        //    var organizationViewModelResult = _mapper.Map<OrganizationViewModel>(result);

        //    return Ok(organizationViewModelResult);
        //}

        //todo put organization with validation on lat/long and org type.
        // then test.
        [ODataRoute("({organizationId})")]
        [HttpPut]
        public async Task<IActionResult> Put(Guid organizationId, [FromBody] OrganizationViewModel organizationViewModel)
        {       
            var organization = _mapper.Map<Organization>(organizationViewModel);

            var result = await _organizationService.UpdateOrganizationAsync(organization);

            var organizationViewModelResult = _mapper.Map<OrganizationViewModel>(result);

            return Ok(organizationViewModelResult);
        }

        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(15728640)] // 15 mb
        public async Task<IActionResult> Upload()
        {
            var fileUploadResult = await _fileProcessingService.UploadMediaAsync(Request, MediaTypes.Logo);

            if (fileUploadResult.IsFailure)
            {
                _logger.LogError($"Error on file Upload {JsonConvert.SerializeObject(fileUploadResult)}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            var organization = _mapper.Map<Organization>(fileUploadResult.Value);

            var addedOrganization = await _organizationService.CreateOrganizationAsync(organization);

            var organizationViewModel = _mapper.Map<OrganizationViewModel>(addedOrganization);

            return Created(organizationViewModel);
        }
    }
}
