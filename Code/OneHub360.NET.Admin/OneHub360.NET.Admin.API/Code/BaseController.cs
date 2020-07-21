/*
 * This code belongs to Hewlett Packard Enterprise
 * Copyright © 2016 HPE -  All rights are reserved worldwide
 */

using NHibernate;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OneHub360.NET.Admin.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController<E, R> : ApiController where E : INHEntity where R : NHEntityRepository<E>, new()
    {
        [HttpGet]
        public virtual HttpResponseMessage GetAll()
        {
            R entityRepository = new R();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.GetAll();
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
        public virtual HttpResponseMessage GetById(Guid id)
        {
            R entityRepository = new R();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entity = entityRepository.GetById(id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entity);
            }
        }

        [HttpPost]
        public virtual void Create([FromBody]E entity)
        {
            if (entity != null)
            {
                R entityRepository = new R();
                entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
                entity.CreationDate = DateTime.Now;
                entity.LastModified = DateTime.Now;
                entity.LastModifiedBy = Identity.UserID();
                entity.CreatedBy = Identity.UserID();
                entityRepository.Insert(entity);
            }
        }

        [HttpPut]
        public virtual HttpResponseMessage Update([FromUri]Guid id, [FromBody]E entity)
        {
            if (entity == null || entity.Id != id)
                return Request.CreateResponse(HttpStatusCode.NotFound, entity);

            R entityRepository = new R();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = Identity.UserID();
            entityRepository.Update(entity);
            return GetAll();
        }

        [HttpDelete]
        public void Delete([FromUri]Guid id)
        {
            R entityRepository = new R();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            entityRepository.Delete(id);
        }
    }
}
