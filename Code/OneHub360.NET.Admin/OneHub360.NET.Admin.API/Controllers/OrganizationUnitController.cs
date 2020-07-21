using NHibernate;
using NHibernate.Cfg;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using NHibernate.Criterion;
using System.IO;
using System.Web;
using NHibernate.SqlCommand;


namespace OneHub360.NET.Admin.API.Controllers
{
    public class OrganizationUnitController : BaseController<OrganizationUnit, OrganizationUnitRepository>
    {

        [HttpPost]
        public override void Create([FromBody]OrganizationUnit entity)
        {
            if (entity != null)
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitTypeID))
                        {
                            var OrganizationUnitTypeID = new Guid(entity.OrganizationUnitTypeID);
                            OrganizationUnitType organizationType = Session.QueryOver<OrganizationUnitType>().Where(x => x.Id == OrganizationUnitTypeID).SingleOrDefault();
                            entity.OrganizationUnitType = organizationType;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitParentID))
                        {
                            var OrganizationUnitParentID = new Guid(entity.OrganizationUnitParentID);
                            OrganizationUnit OrganizationUnitParent = Session.QueryOver<OrganizationUnit>().Where(x => x.Id == OrganizationUnitParentID).SingleOrDefault();
                            entity.OrganizationUnitParent = OrganizationUnitParent;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationID))
                        {
                            Guid OrganizationID;
                            Organization organization;
                            if (entity.OrganizationID == "0")
                            {

                                var Organizationentity = Session.QueryOver<Organization>().Where(x => x.IsLocal == true).SingleOrDefault();

                                OrganizationID = Organizationentity.Id;
                            }
                            else
                            {
                                OrganizationID = new Guid(entity.OrganizationID);
                            }
                            organization = Session.QueryOver<Organization>().Where(x => x.Id == OrganizationID).SingleOrDefault();
                            
                            entity.Organization = organization;
                        }

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
        public override HttpResponseMessage Update([FromUri]Guid id, [FromBody]OrganizationUnit entity)
        {
            if (entity != null && entity.Id != null)

            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitTypeID))
                        {
                            var OrganizationUnitTypeID = new Guid(entity.OrganizationUnitTypeID);
                            OrganizationUnitType organizationType = Session.QueryOver<OrganizationUnitType>().Where(x => x.Id == OrganizationUnitTypeID).SingleOrDefault();
                            entity.OrganizationUnitType = organizationType;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitParentID))
                        {
                            var OrganizationUnitParentID = new Guid(entity.OrganizationUnitParentID);
                            OrganizationUnit OrganizationUnitParent = Session.QueryOver<OrganizationUnit>().Where(x => x.Id == OrganizationUnitParentID).SingleOrDefault();
                            entity.OrganizationUnitParent = OrganizationUnitParent;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationID))
                        {
                            var OrganizationID = new Guid(entity.OrganizationID);
                            Organization Organization = Session.QueryOver<Organization>().Where(x => x.Id == OrganizationID).SingleOrDefault();
                            entity.Organization = Organization;
                        }

                        if (entity.CreatedBy == null)
                            entity.CreatedBy = Identity.UserID();
                        if (entity.LastModified == null)
                            entity.LastModified = DateTime.Now;

                        entity.LastModified = DateTime.Now;
                        entity.LastModifiedBy = Identity.UserID();

                        Session.Update(entity);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);


                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, GetAll());
        }


        [HttpGet]
        public HttpResponseMessage getbyorganizationID(string ID)
        {
            OrganizationUnitRepository entityRepository = new OrganizationUnitRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationUnit>().Where(x => x.Organization.Id == new Guid(ID)).List();

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].OrganizationUnitType != null)
                        entities[i].OrganizationUnitTypeID = entities[i].OrganizationUnitType.Id.ToString();

                    if (entities[i].Organization != null)
                        entities[i].OrganizationID = entities[i].Organization.Id.ToString();

                    if (entities[i].OrganizationUnitParent != null)
                        entities[i].OrganizationUnitParentID = entities[i].OrganizationUnitParent.Id.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entities);
            }
        }
        public override HttpResponseMessage GetAll()
        {
            OrganizationUnitRepository entityRepository = new OrganizationUnitRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationUnit>()
            .JoinQueryOver(p => p.Organization)
            .Where(c => c.IsLocal == false).List();
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].OrganizationUnitType != null)
                        entities[i].OrganizationUnitTypeID = entities[i].OrganizationUnitType.Id.ToString();

                    if (entities[i].Organization != null)
                        entities[i].OrganizationID = entities[i].Organization.Id.ToString();

                    if (entities[i].OrganizationUnitParent != null)
                        entities[i].OrganizationUnitParentID = entities[i].OrganizationUnitParent.Id.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entities);
            }
        }
        [HttpGet]

        public  HttpResponseMessage GetLocal()
        {
            OrganizationUnitRepository entityRepository = new OrganizationUnitRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationUnit>().JoinQueryOver(p => p.Organization)
            .Where(c => c.IsLocal == true).List();
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].OrganizationUnitType != null)
                        entities[i].OrganizationUnitTypeID = entities[i].OrganizationUnitType.Id.ToString();

                    if (entities[i].Organization != null)
                        entities[i].OrganizationID = entities[i].Organization.Id.ToString();

                    if (entities[i].OrganizationUnitParent != null)
                        entities[i].OrganizationUnitParentID = entities[i].OrganizationUnitParent.Id.ToString();
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
            OrganizationUnitRepository entityRepository = new OrganizationUnitRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<OrganizationUnit>()
           .Where
           (Restrictions.On<OrganizationUnit>(x => x.Title).IsLike("%" + keyword + "%") ||
            Restrictions.On<OrganizationUnit>(x => x.Location).IsLike("%" + keyword + "%") ||
            Restrictions.On<OrganizationUnit>(x => x.About).IsLike("%" + keyword + "%"))
            .List();


            if (entities != null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entities);
            }
        }
        [HttpGet]

        public virtual HttpResponseMessage picture(Guid id)
        {
            OrganizationUnitRepository entityRepository = new OrganizationUnitRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var user = entityRepository.GetById(id);

            if (user.Logo == null)
            {
                user.Logo = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/img/components/organizationautocomplete/nopic.png"));
            }

            var stream = new MemoryStream(user.Logo);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
    }

}

