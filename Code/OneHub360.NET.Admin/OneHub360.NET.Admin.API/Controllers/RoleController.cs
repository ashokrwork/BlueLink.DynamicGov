using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class RoleController : BaseController<Role, RoleRepository>
    {
        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            RoleRepository entityRepository = new RoleRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<Role>()
           .Where
           (Restrictions.On<Role>(x => x.Name).IsLike("%" + keyword + "%") ||
           Restrictions.On<Role>(x => x.Description).IsLike("%" + keyword + "%"))
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
        [HttpPost]
        public virtual HttpResponseMessage adduser(string userid, string Roleid)
        {
            if (!string.IsNullOrEmpty(userid))
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        // Role role = Session.QueryOver<Role>().Where(x => x.Id == new Guid(Roleid)).SingleOrDefault();
                        //UserInfo user = Session.QueryOver<UserInfo>().Where(x => x.Id == new Guid(userid)).SingleOrDefault();

                        ////role.Users.Add(user);
                        Rolesusers RU = new Rolesusers();
                        RU.FkRole = new Guid(Roleid);
                        RU.FkUserinfo = new Guid(userid);

                        RU.CreationDate = DateTime.Now;
                        RU.LastModified = DateTime.Now;
                        RU.LastModifiedBy = Identity.UserID();
                        RU.CreatedBy = Identity.UserID();

                        //Rolesusers RU = new Rolesusers();
                        //RolesusersRepository entityRepository = new RolesusersRepository();
                        //RU.FkRole = new Guid( Roleid);
                        //RU.FkUserinfo = new Guid(userid);
                        object ob =Session.Save(RU);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);

        }
        [HttpDelete]
        public void DeleteUser(string userid, string Roleid)
        {
            if (!string.IsNullOrEmpty(userid))
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {

                        Rolesusers RU = new Rolesusers();

                        RU.FkRole = new Guid(Roleid);
                        RU.FkUserinfo = new Guid(userid);

                        RU = Session.QueryOver<Rolesusers>().Where(x => x.FkUserinfo == new Guid(userid)).Where(x => x.FkRole == new Guid(Roleid)).SingleOrDefault();
                        Session.Delete(RU);
                        tx.Commit();
                    }
                }
            }
        }
        public  HttpResponseMessage GetUserRole( string Roleid)
        {
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();
            List<UserInfo> UIList = new List<UserInfo>();
            using (ISession Session = sessionFactory.OpenSession())
            {
                using (ITransaction tx = Session.BeginTransaction())
                {

                    if (Roleid != null)
                    {
                       var RUList = Session.QueryOver<Rolesusers>().Where(x => x.FkRole == new Guid(Roleid)).List();

                        for (int i = 0; i < RUList.Count; i++)
                        {
                            UIList.Add(Session.QueryOver<UserInfo>().Where(x => x.Id == RUList[i].FkUserinfo).SingleOrDefault());
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, UIList);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, UIList);
                    }
                }
            }
        }
        public override HttpResponseMessage GetAll()
        {
            RoleRepository entityRepository = new RoleRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.GetAll();

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                   

                }
                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, entities);
            }
        }
    }
}
