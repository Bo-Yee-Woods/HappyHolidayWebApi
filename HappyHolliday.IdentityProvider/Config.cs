using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyHolliday.IdentityProvider
{
    public static class Config
    {
        public static List<TestUser> getTestUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1234567765434567876",
                    Username = "Frank",
                    Password = "pwd",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Woods"),
                    },
                },
                new TestUser
                {
                    SubjectId = "123456123124567876",
                    Username = "Franklin",
                    Password = "pwd123",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Franklin"),
                        new Claim("family_name", "lin"),
                    },
                },
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<Client> GetClients(string domainName)
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Happy_Holiday_Client",
                    ClientName = "Happy Holiday Client",

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    //RequireConsent = true,

                    RedirectUris           = { $"{domainName}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{domainName}/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "role",
                    },

                    ClientSecrets =
                    {
                        new Secret("123".Sha256())
                    },

                    AlwaysIncludeUserClaimsInIdToken = true
                },

            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
            };
        }
    }
}
