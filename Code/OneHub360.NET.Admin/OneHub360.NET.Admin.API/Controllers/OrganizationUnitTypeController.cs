using NHibernate.Criterion;
using OneHub360.NET.Admin.Model;
using System.Net.Http;
using System.Web.Http;
namespace OneHub360.NET.Admin.API.Controllers
{
    public class OrganizationUnitTypeController : BaseController<OrganizationUnitType, OrganizationUnitTypeRepository>
    {
        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            OrganizationUnitTypeRepository entityRepository = new OrganizationUnitTypeRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationUnitType>()
           .Where
           (Restrictions.On<OrganizationUnitType>(x => x.Name).IsLike("%" + keyword + "%"))
            .List();


            if (entities != null)
            {

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, entities);
            }
        }
    }
}
