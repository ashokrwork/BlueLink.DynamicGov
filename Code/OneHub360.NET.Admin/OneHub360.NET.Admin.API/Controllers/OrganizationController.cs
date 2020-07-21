/*
 * This code belongs to Hewlett Packard Enterprise
 * Copyright © 2016 HPE -  All rights are reserved worldwide
 */

using NHibernate;
using NHibernate.Cfg;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using NHibernate.Criterion;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class OrganizationController : BaseController<Organization, OrganizationRepository>
    {
        [HttpPost]
        public override void Create([FromBody]Organization entity)
        {
            if (entity != null)
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        var OrganizationTypeID = new Guid(entity.OrganizationTypeID);
                        OrganizationType organizationType = Session.QueryOver<OrganizationType>().Where(x => x.Id == OrganizationTypeID).SingleOrDefault();
                        entity.OrganizationType = organizationType;
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
        public override HttpResponseMessage Update([FromUri]Guid id, [FromBody]Organization entity)
        {
            if (entity != null && entity.Id != null)

            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        var OrganizationTypeID = new Guid(entity.OrganizationTypeID);
                        OrganizationType organizationType = Session.QueryOver<OrganizationType>().Where(x => x.Id == OrganizationTypeID).SingleOrDefault();
                        entity.OrganizationType = organizationType;
                        entity.LastModified = DateTime.Now;
                        entity.LastModifiedBy = Identity.UserID();

                        Session.Update(entity);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, GetAll());


                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, GetAll());
        }
        public override HttpResponseMessage GetAll()
        {
            OrganizationRepository entityRepository = new OrganizationRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<Organization>().Where(x => x.IsLocal == false).List();

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].OrganizationType != null)
                        entities[i].OrganizationTypeID = entities[i].OrganizationType.Id.ToString();
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
            OrganizationRepository entityRepository = new OrganizationRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<Organization>()
           .Where
           (Restrictions.On<Organization>(x => x.Name).IsLike("%" + keyword + "%") ||
            Restrictions.On<Organization>(x => x.Email).IsLike("%" + keyword + "%"))
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
    }
}
