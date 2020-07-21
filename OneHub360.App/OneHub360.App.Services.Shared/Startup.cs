using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OneHub360.App.Services.Shared;
using OneHub360.Authentication.Providers;
using Owin;
using System;
using System.Web.Http;

namespace OneHub360.App.Services
{
    public  class Startup
    {
        public  static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public  void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        
    }
}