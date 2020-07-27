using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OneHub360.CMS.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
            };
        }
    }
}
