using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.User;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.User;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.UsersRoute)]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(ODataQueryOptions<UserViewModel> odataOptions)
        {
            var result = new List<UserViewModel>();

            var organizationQueryable = _userService.GetAll();

            var projectedUsers = organizationQueryable.ProjectTo<UserViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedUsers);

            var userViewModels = appliedResults as IQueryable<UserViewModel>;

            if (userViewModels != null)
            {
                result = await userViewModels.ToListAsync();
            }

            return Ok(result);
        }

        [ODataRoute]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel userViewModel)
        {
            //todo Add authorization check here after user/role etc is added.
            //global add user permission
            var user = _mapper.Map<User>(userViewModel);

            var result = await _userService.CreateAsync(user, userViewModel.Password);

            var userViewModelResult = _mapper.Map<UserViewModel>(result);

            return StatusCode((int)HttpStatusCode.Created, userViewModelResult);
        }

        [ODataRoute("({userId})")]
        [HttpPut]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserViewModel userViewModel)
        {
            //todo: add global update user permission

            if (userId != userViewModel.Id)
            {
                throw new HttpBadRequestException(ErrorMessages.IdsMisMatch);
            }

            var existingUser = await _userService.GetByIdWithNoTrackingAsync(userId);
            if (existingUser == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var user = _mapper.Map(userViewModel, existingUser);

            var result = await _userService.UpdateAsync(userId, user);

            var userViewModelResult = _mapper.Map<UserViewModel>(result);

            return Ok(userViewModelResult);
        }

        [ODataRoute("({userId})")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}
