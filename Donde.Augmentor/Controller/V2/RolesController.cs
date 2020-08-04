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
    [ODataRoutePrefix(ODataConstants.RolesRoute)]
    [Authorize]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService,
            IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(ODataQueryOptions<RoleViewModel> odataOptions)
        {
            var result = new List<RoleViewModel>();

            var organizationQueryable = _roleService.GetAll();

            var projectedRoles = organizationQueryable.ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedRoles);

            var roleViewModels = appliedResults as IQueryable<RoleViewModel>;

            if (roleViewModels != null)
            {
                result = await roleViewModels.ToListAsync();
            }

            return Ok(result);
        }
    }
}
