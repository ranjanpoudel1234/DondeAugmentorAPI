using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //enables identity part
                new IdentityResources.Email(), // tokenservice exposes email resource.
                new IdentityResources.Profile(), //claims like name, birthdate
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("donde-api", "Donde API")
                {
                    Scopes = {new Scope("donde-api-read"), new Scope("donde-api-write")}
                }
            };
        }

        public static IEnumerable<Client> GetClients(string devHost = "")
        {
            var clients = new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "AugmentUWeb",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                      ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },
                    AllowedScopes = { "openid", "profile", "email", "donde-api-read" },
                   // RedirectUris = {$"http://{devHost}/test-client/callback.html"}, // test client runs on same host
                   // AllowedCorsOrigins = {$"http://{devHost}" }, // test client runs on same host
                   // AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                 new Client {
                    RequireConsent = false,
                    ClientId = "AugmentUMobile",
                    ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "donde-api-read" },
                   // RedirectUris = {"http://localhost:4200/auth-callback"}, // test client runs on same host,                 
                }
            };

            return clients;
        }
    }
}
