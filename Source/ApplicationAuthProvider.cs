using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace WebAPIOwinAuth
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (string.IsNullOrEmpty(context.UserName))
            {
                context.SetError("invalid_grant", "Please enter username");
                return;
            }

            if (string.IsNullOrEmpty(context.Password))
            {
                context.SetError("invalid_grant", "Please enter password");
                return;
            }

            bool Valid = false;
            //users will be validated from database after data access layer is generated, below is temporary code...
            if (context.UserName == "admin" && context.Password == "admin1234")
            {
                Valid = true;
            }
            if (Valid)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Username", context.UserName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }
        }
    }
}