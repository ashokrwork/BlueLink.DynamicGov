using NHibernate.Criterion;
using OneHub360.NET.Admin.Model;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OneHub360.NET.Admin.API.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JobTitleController : BaseController<JobTitle, JobTitleRepository>
    {

        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            JobTitleRepository entityRepository = new JobTitleRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<JobTitle>()
           .Where
           (Restrictions.On<JobTitle>(x => x.Title).IsLike("%" + keyword + "%") ||
           Restrictions.On<JobTitle>(x => x.Responsibilities).IsLike("%" + keyword + "%") ||
           Restrictions.On<JobTitle>(x => x.Description).IsLike("%" + keyword + "%"))
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
