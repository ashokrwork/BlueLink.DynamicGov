using Owin;
using System.Web.Http;

namespace OneHub360.App.Web
{
    public  class Startup
    {
        //public  static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public  void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //ConfigureOAuth(app);

            WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        //public virtual void ConfigureOAuth(IAppBuilder app)
        //{
        //    OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

        //    OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
        //    {

        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/services/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
        //        Provider = 
        //        new OneHub360AuthorizationServerProvider<OneHub360UserManager>
        //        (new OneHub360UserManager())
        //    };

        //    app.UseOAuthAuthorizationServer(OAuthServerOptions);
        //    app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        //}
    }
}