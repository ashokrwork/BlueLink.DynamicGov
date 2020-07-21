using System.Web;
using System.Web.Mvc;

namespace OneHub360.NET.Admin.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
