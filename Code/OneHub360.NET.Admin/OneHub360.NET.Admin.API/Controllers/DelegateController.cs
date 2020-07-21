using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class DelegateController : BaseController<Delegates, DelegateRepository>
    {
        [HttpPost]
        public override void Create([FromBody]Delegates entity)
        {
            if (entity != null)
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        UserInfo Delegate = Session.QueryOver<UserInfo>().Where(x => x.Id == entity.Delegateid).SingleOrDefault();
                        UserInfo Delegator = Session.QueryOver<UserInfo>().Where(x => x.Id == entity.Delegatorid).SingleOrDefault();
                        entity.DelegatorUser = Delegator;
                        entity.DelegeteUser = Delegate;
                        entity.CreationDate = DateTime.Now;
                        entity.LastModified = DateTime.Now;
                        entity.LastModifiedBy = Identity.UserID();
                        entity.CreatedBy = Identity.UserID();

                        Session.Save(entity);
                        tx.Commit();
                    }
                }
            }
        }
        [HttpPut]
        public override HttpResponseMessage Update([FromUri]Guid id, [FromBody]Delegates entity)
        {
            if (entity != null && entity.Id != null)

            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        UserInfo Delegate = Session.QueryOver<UserInfo>().Where(x => x.Id == entity.Delegateid).SingleOrDefault();
                        UserInfo Delegator = Session.QueryOver<UserInfo>().Where(x => x.Id == entity.Delegatorid).SingleOrDefault();
                        entity.DelegatorUser = Delegator;
                        entity.DelegeteUser = Delegate;
                        entity.Delegatorid = Delegator.Id;
                        entity.Delegateid = Delegate.Id;

                        entity.LastModified = DateTime.Now;
                        entity.LastModifiedBy = Identity.UserID();

                        Session.Update(entity);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, "updated");


                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, GetAll());
        }
        public override HttpResponseMessage GetAll()
        {
            DelegateRepository entityRepository = new DelegateRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.GetAll();

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].Delegatorid != null)
                    {
                        UserInfo Delegate = entityRepository.Session.QueryOver<UserInfo>().Where(x => x.Id == entities[i].Delegateid).SingleOrDefault();
                        UserInfo Delegator = entityRepository.Session.QueryOver<UserInfo>().Where(x => x.Id == entities[i].Delegatorid).SingleOrDefault();
                        entities[i].DelegatorUser = Delegator;
                        entities[i].DelegeteUser = Delegate;
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entities);
            }
        }
        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            DelegateRepository entityRepository = new DelegateRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<Model.Delegates>()
           .Where
           (Restrictions.On<Model.Delegates>(x => x.DelegatorUser.ArabicFullName).IsLike("%" + keyword + "%") )
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
