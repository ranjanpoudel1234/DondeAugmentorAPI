using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Web.ViewModels.V1.Account;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class AccountsController : BaseController
    {
        private readonly SignInManager<User> _signInManager; 
        private readonly UserManager<User> _userManager;


        public AccountsController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;  
        }

        //// this code will be removed soon.
        //// Leaving here to help us add superAdmin if needed.
        //[HttpPost]
        //[Route("api/v1/accounts/register")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody]AccountViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = new User { UserName = model.Email, Email = model.Email };

        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));

        //    return Ok(user); //todo map user back
        //}

        [Route("api/v1/accounts/userInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");

            if (userIdClaim != null)
            {
                var currentLoggedInUser = await _userManager.FindByIdAsync(userIdClaim.Value);

                if (currentLoggedInUser.IsDeleted)
                {
                    throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
                }

                return Ok(currentLoggedInUser);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Getting user info");
        }


        [HttpPost("api/v1/accounts/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
