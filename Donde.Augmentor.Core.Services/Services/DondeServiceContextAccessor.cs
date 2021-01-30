using Donde.Augmentor.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Donde.Augmentor.Core.Services.Services
{
    public class DondeServiceContextAccessor : IDondeHttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DondeServiceContextAccessor(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?.Value; ;
        }
    }
}
