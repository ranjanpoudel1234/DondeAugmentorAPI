using Donde.Augmentor.Core.Domain.CustomExceptions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Donde.Augmentor.Web.Controller
{

    public class BaseController : ODataController
    {
        private const string DONDE_AUGMENTOR_ORGANIZATION_ID = "DONDE-AUGMENTOR-ORGANIZATION-ID";
        private const string HEADER_AUTHORIZATION_ID = "HEADER-AUTHORIZATION-ID";

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

        public void AuthorizeByHeader()
        {
            var headerAuthValue = HttpContext.Request?.Headers[HEADER_AUTHORIZATION_ID];

            if (!headerAuthValue.HasValue || !headerAuthValue.Value.Equals("U/GeVksbUOZ0CwFyPCigiEa5E40zHmav/LLYC8LoiimGZ8DUwBYc8M/H/eGFAgqluBQcT0/gHcN6NUPDD88xIg=="))
            {
                throw new HttpUnauthorizedException("Not Authorized");
            }
        }
    }
}
