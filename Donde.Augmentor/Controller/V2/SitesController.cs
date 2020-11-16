using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.CustomExceptions;
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
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [Authorize]
    [ODataRoutePrefix(ODataConstants.SitesRoute)]
    public class SitesController : BaseController
    {
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public SitesController(ISiteService siteService,
            IMapper mapper)
        {
            _siteService = siteService;
            _mapper = mapper;         
        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(ODataQueryOptions<SiteViewModel> odataOptions)
        {
            var result = new List<SiteViewModel>();

            var organizationQueryable = _siteService.GetSites();

            var projectedSites = organizationQueryable.ProjectTo<SiteViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedSites);

            var siteViewModels = appliedResults as IQueryable<SiteViewModel>;

            if (siteViewModels != null)
            {
                result = await siteViewModels.ToListAsync();
            }

            return Ok(result);
        }

        [ODataRoute]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteViewModel siteViewModel)
        {
            var organizationId = GetCurrentOrganizationIdOrThrow();

            if (organizationId != siteViewModel.OrganizationId)
            {
                throw new HttpBadRequestException(DondeErrorMessages.INVALID_ORGANIZATION_ID);
            }

            //todo Add authorization check here after user/role etc is added.
            var site = _mapper.Map<Site>(siteViewModel);

            var result = await _siteService.CreateSiteAsync(site);

            var siteViewModelResult = _mapper.Map<SiteViewModel>(result);

            return StatusCode((int)HttpStatusCode.Created, siteViewModelResult);
        }
    }
 }
