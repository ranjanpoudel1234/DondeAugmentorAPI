using AutoMapper;
using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.User;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.User;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.UserInfo)]
    [Authorize]
    public class UserInfoController : BaseController
    {
     
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDondeHttpContextAccessor _dondeHttpContextAccessor;

        public UserInfoController(IUserService userService, IMapper mapper, IDondeHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _dondeHttpContextAccessor = contextAccessor;
        }

        [HttpGet]
        [ODataRoute]
        public async Task<IActionResult> Get()
        {
            var userId = _dondeHttpContextAccessor.GetCurrentUserId();

            var result = await _userService.GetByIdAsync(Guid.Parse(userId));

            var userInfo = _mapper.Map<UserInfoViewModel>(result);

            return Ok(userInfo);
        }
    }
}
