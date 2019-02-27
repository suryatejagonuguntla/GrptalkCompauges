using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(CompaugesWebApi.Startup))]

namespace CompaugesWebApi
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            Logger.TraceLog("start up came ");
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //Configuration(app);

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            Logger.TraceLog("start up last ");
        }
    }
    
}
