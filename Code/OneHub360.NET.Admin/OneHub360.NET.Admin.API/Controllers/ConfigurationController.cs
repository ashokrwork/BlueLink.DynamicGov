using NHibernate.Criterion;
using OneHub360.NET.Admin.Model;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OneHub360.NET.Admin.API.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigurationController : BaseController<CustomConfiguration, ConfigurationRepository>
    {

      
    }
}
