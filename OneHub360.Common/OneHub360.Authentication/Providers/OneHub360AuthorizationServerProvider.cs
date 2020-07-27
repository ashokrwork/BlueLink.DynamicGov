using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;
using OneHub360.Authentication.Manager;

namespace OneHub360.Authentication.Providers
{
    public class OneHub360AuthorizationServerProvider<T> 
        : OAuthAuthorizationServerProvider where T : IClaimsUserManager
    {
        private IClaimsUserManager ClaimsUserManager;
        public OneHub360AuthorizationServerProvider(T cliamsUserManager)
        {
            ClaimsUserManager = cliamsUserManager;
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user = ClaimsUserManager.Find(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", user.Id));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "userId", user.Id
                    },
                    {
                        "userName", user.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }



        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.OwinContext.Set<string>("as:clientAllowedOrigin","*");
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", "14400");

            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}