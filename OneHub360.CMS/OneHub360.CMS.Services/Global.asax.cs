using OneHub360.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace OneHub360.CMS.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
