using Donde.Augmentor.Web.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.UsersRoute)]
    [Authorize]
    public class UsersController
    {
    }
}
