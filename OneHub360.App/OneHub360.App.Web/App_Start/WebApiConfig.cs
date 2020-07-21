using Newtonsoft.Json;
using System.Web.Http;

namespace OneHub360.App.Web
{
    public  static class WebApiConfig
    {
        public  static void Register(HttpConfiguration config)
        {
            

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
            //json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            //var cors = new EnableCorsAttribute("*", "*", "*");

            //config.EnableCors(cors);

            

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            
        }
    }
}
