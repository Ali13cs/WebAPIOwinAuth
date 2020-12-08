using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebAPIOwinAuth.App_Start.OWINAuthStarup))]

namespace WebAPIOwinAuth.App_Start
{
    public class OWINAuthStarup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, //visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);
            var TokenExpirationTime = System.Web.Configuration.WebConfigurationManager.AppSettings["TokenExpirationMinutes"];
            double TokenExpirationMinutes = 30;
            if (!string.IsNullOrEmpty(TokenExpirationTime))
            {
                try
                {
                    TokenExpirationMinutes = Convert.ToInt64(TokenExpirationTime);
                }
                catch (Exception ex)
                {
                    TokenExpirationMinutes = 30;
                }
            }
            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(TokenExpirationMinutes),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
