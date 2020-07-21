using NHibernate.Criterion;
using OneHub360.NET.Admin.Model;
using System.Net.Http;
using System.Web.Http;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class OrganizationTypeController : BaseController<OrganizationType, OrganizationTypeRepository>
    {

        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            OrganizationTypeRepository entityRepository = new OrganizationTypeRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationType>()
           .Where
           (Restrictions.On<OrganizationType>(x => x.Name).IsLike("%" + keyword + "%") )
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
