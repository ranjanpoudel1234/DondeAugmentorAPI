using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.Organization;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Donde.Augmentor.Web.Attributes.IgnoreJsonIgnore;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.OrganizationRoute)]
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        public readonly DomainSettings _domainSettings;

        public OrganizationsController(IOrganizationService organizationService, IMapper mapper, DomainSettings domainSettings)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _domainSettings = domainSettings;
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

            result.ForEach(x => x.Logo.Url = GetPathWithRootLocationOrNull(x.Logo.Url));

            return Ok(result);
        }
   
        [ODataRoute]
        [HttpPost]
        [IgnoreJsonIgnore]
        public async Task<IActionResult> Post([FromBody] OrganizationViewModel organizationViewModel)
        {       
            //todo Add authorization check here after user/role etc is added.
            var organization = _mapper.Map<Organization>(organizationViewModel);

            var result = await _organizationService.CreateOrganizationAsync(organization);

            var organizationViewModelResult = _mapper.Map<OrganizationViewModel>(result);

            organizationViewModelResult.Logo.Url = GetPathWithRootLocationOrNull(organizationViewModelResult.Logo.Url);

            return StatusCode((int)HttpStatusCode.Created, organizationViewModelResult);
        }

        private string GetPathWithRootLocationOrNull(string relativeUrl)
        {
            if (relativeUrl == null) return null;

            return $"{_domainSettings.GeneralSettings.StorageBasePath}{relativeUrl}";
        }
    }
}
