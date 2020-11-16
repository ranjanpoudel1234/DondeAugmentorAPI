using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.RoleAndPermission;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.PermissionsRoute)]
    [Authorize]
    public class PermissionsController : BaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionsController(IPermissionService permissionService,
            IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> GetAll(ODataQueryOptions<PermissionViewModel> odataOptions)
        {
            var result = new List<PermissionViewModel>();

            var organizationQueryable = _permissionService.GetAll();

            var projectedPermissions = organizationQueryable.ProjectTo<PermissionViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedPermissions);

            var permissionViewModels = appliedResults as IQueryable<PermissionViewModel>;

            if (permissionViewModels != null)
            {
                result = await permissionViewModels.ToListAsync();
            }

            return Ok(result);
        }
    }
}
