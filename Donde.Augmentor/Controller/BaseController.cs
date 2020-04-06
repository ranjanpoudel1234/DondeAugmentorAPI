using Donde.Augmentor.Core.Domain.CustomExceptions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Donde.Augmentor.Web.Controller
{

    public class BaseController : ODataController
    {
        private const string DONDE_AUGMENTOR_ORGANIZATION_ID = "DONDE-AUGMENTOR-ORGANIZATION-ID";
        private const string METRIC_ACCESS_ID = "METRIC-ACCESS-ID";

        public Guid GetCurrentOrganizationIdOrThrow()
        {
            var organizationId = HttpContext.Request?.Headers[DONDE_AUGMENTOR_ORGANIZATION_ID];

            if (!organizationId.HasValue)
            {
                throw new HttpBadRequestException("Current OrganizationId Not Found");
            }

            Guid result;
            if(Guid.TryParse(organizationId.Value, out result))
            {
                return result;
            }
            else
            {
                throw new HttpBadRequestException("Current OrganizationId Not Found");
            }
        }

        // @todo, later, per requirement, update this to use token authentication
        // idea, on app load, register user with their uniqueId as username and generated password. 
        // This can change if we have actual user registration later, hence keeping it simple for now.
        // app will make request with that info and client info to get the token,
        // check token expiration and make api requests. Unnecessary overhead for now.
        public void AuthorizeTemporariLyByHeaderOrThrow()
        {
            var headerAuthValue = HttpContext.Request?.Headers[METRIC_ACCESS_ID];

            if (!headerAuthValue.HasValue || !headerAuthValue.Value.Equals("OF00aofFI+5xPiQWVoIyxfLqrddlKIRcrYgKm6oVzbuzO91MQScbFzx9weiIXDqMNSodnyHPSfgsmhYMBxFuxo+l2sPoW0ojPyfCWkYF3jujSmAhJ7u5Dp91BujXHwcZ+4XnKVBi9BPL93onSk3nU55+fviMewSBAAxsiz+5ne31C/4oAimejLJMOYz/36GRhfLWSoPBfXHNDxBj8hWYqrkxWDceSAJmgcjGv2ncBBpVP/tkG0eiN7kTSmIuOrsNGtVMq/P3hG/28xCR5FNRl/jbqKykf0m8UX9ScoX1VDevq3tvTSWK6ziSCGaEPSMJGLeGpA6oEr94CWO69Hlp"))
            {
                throw new HttpUnauthorizedException("Not Authorized");
            }
        }
    }
}
