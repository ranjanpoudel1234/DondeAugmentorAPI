using Donde.Augmentor.Core.Domain.CustomExceptions;
using Microsoft.AspNet.OData;
using System;

namespace Donde.Augmentor.Web.Controller
{
    public class BaseController : ODataController
    {
        private const string DONDE_AUGMENTOR_ORGANIZATION_ID = "DONDE-AUGMENTOR-ORGANIZATION-ID";

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
    }
}
