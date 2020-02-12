using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Infrastructure.Database.Identity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Donde.Augmentor.Web.Identity
{
    public static class IdentityServiceCollectionExtension
    {
        public static void AddDondeIdentityServer(this IServiceCollection services, bool isLocalEnvironment, Client[] clients)
        {
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DondeIdentityContext>();

            var serviceBuilder = services.AddIdentityServer()
                          .AddInMemoryIdentityResources(Config.GetIdentityResources())
                          .AddInMemoryApiResources(Config.GetApiResources())
                          .AddInMemoryClients(Config.GetClients(clients))
                          .AddAspNetIdentity<User>();

            if (isLocalEnvironment)
            {
                serviceBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }
    }
}
