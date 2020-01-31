using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Web.ViewModels.Account;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("accounts")]
    public class AccountsController : BaseController
    {
        private readonly SignInManager<User> _signInManager; 
        private readonly UserManager<User> _userManager;


        public AccountsController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
     
        }
        [ODataRoute]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { UserName = model.Email, FullName = model.FullName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.FullName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", model.Role));

            return Ok(user); //todo map user back
        }
    }
}
