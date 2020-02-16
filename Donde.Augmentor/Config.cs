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

        public static IEnumerable<Client> GetClients(Client[] dondeClients)
        {
            var clients = new List<Client>();
            foreach(var client in dondeClients)
            {
                clients.Add(new Client
                {
                    RequireConsent = client.RequireConsent,
                    ClientId = client.ClientId,
                    AllowedGrantTypes = client.AllowedGrantTypes,
                    ClientSecrets = client.ClientSecrets.Select(x => new Secret(x.Value.Sha256())).ToList(),
                    AllowedScopes = client.AllowedScopes,
                    AllowOfflineAccess = client.AllowOfflineAccess,
                    AccessTokenLifetime = client.AccessTokenLifetime
                });
            }
          
            return clients;
        }
    }
}
