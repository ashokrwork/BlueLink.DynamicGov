using NHibernate;
using NHibernate.Cfg;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using NHibernate.Criterion;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class UserInfoController : BaseController<UserInfo, UserInfoRepository>
    {
        [HttpGet]
        public string GetUser()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return userName;

        }
        [HttpGet]
        public HttpResponseMessage GetADUsers()
        {
            List<string> _result = new List<string>(); ;
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings["ADdomain"]))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            //_result+=("First Name: " + de.Properties["givenName"].Value);
                            //_result += ("Last Name : " + de.Properties["sn"].Value);
                            if (de.Properties["userPrincipalName"].Value != null)
                                if (!string.IsNullOrEmpty(de.Properties["userPrincipalName"].Value.ToString().Trim()))
                                    _result.Add(de.Properties["userPrincipalName"].Value.ToString().Replace("@"+ System.Configuration.ConfigurationManager.AppSettings["ADdomain"],""));
                            //_result += ("User principal name: " + de.Properties["userPrincipalName"].Value);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, _result);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound, _result);
            }

        }
        [HttpPost]
        public HttpResponseMessage login([FromBody]UserInfo entity)
        {
            UserInfo user = null;
            if (entity != null)
            {
                //if (ActiveDirectoryAuthenticate(entity.LoginName, entity.Password))
                {
                    var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                    using (ISession Session = sessionFactory.OpenSession())
                    {
                        using (ITransaction tx = Session.BeginTransaction())
                        {

                            //user = Session.QueryOver<UserInfo>().Where(x => x.LoginName == entity.LoginName).SingleOrDefault();
                            user = Session.QueryOver<UserInfo>().Where(x => x.LoginName == entity.LoginName).Where(x => x.Password == entity.Password).SingleOrDefault();
                            if (user != null)
                            {
                                //Query over roles
                                var query = "SELECT Role.Name " +
                                     "FROM RolesUsers INNER JOIN " +
                                     "Role ON RolesUsers.FK_Role = Role.Id INNER JOIN " +
                                     "UserInfo ON RolesUsers.FK_UserInfo = UserInfo.Id "
                            +
                            "WHERE(UserInfo.LoginName = :LoginName) " +
                            "AND(UserInfo.Password = :password) AND(Role.Id = :roleid) ";
                                var queryresult = Session.CreateSQLQuery(query)
                                                .SetString("LoginName", entity.LoginName)
                                                .SetString("password", entity.Password).SetString("roleid", "E2E740F1-CBE8-42B3-8274-A6A9016C2A8C")
                                                .List();
                                if (queryresult.Count == 0)
                                    return Request.CreateResponse(HttpStatusCode.NotFound, "user not found");
                                return Request.CreateResponse(HttpStatusCode.OK, user);

                            }
                            else
                                return Request.CreateResponse(HttpStatusCode.NotFound, "user not found");

                        }
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, user);

        }
        [HttpPost]
        public HttpResponseMessage loginAD([FromBody]UserInfo entity)
        {
            UserInfo user = null;
            if (entity != null)
            {
                //if (ActiveDirectoryAuthenticate(entity.LoginName, entity.Password))
                {
                    var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                    using (ISession Session = sessionFactory.OpenSession())
                    {
                        using (ITransaction tx = Session.BeginTransaction())
                        {

                            user = Session.QueryOver<UserInfo>().Where(x => x.ADUsername == entity.ADUsername).SingleOrDefault();
                            if (user != null)
                            {
                                //Query over roles
                                var query = "SELECT Role.Name " +
                                     "FROM RolesUsers INNER JOIN " +
                                     "Role ON RolesUsers.FK_Role = Role.Id INNER JOIN " +
                                     "UserInfo ON RolesUsers.FK_UserInfo = UserInfo.Id "
                            +
                            "WHERE(UserInfo.LoginName = :LoginName) " +
                            "AND(UserInfo.Password = :password) AND(Role.Id = :roleid) ";
                                var queryresult = Session.CreateSQLQuery(query)
                                                .SetString("LoginName", entity.LoginName)
                                                .SetString("password", entity.Password).SetString("roleid", "E2E740F1-CBE8-42B3-8274-A6A9016C2A8C")
                                                .List();
                                if (queryresult.Count == 0)
                                    return Request.CreateResponse(HttpStatusCode.NotFound, "user not found");
                                return Request.CreateResponse(HttpStatusCode.OK, user);

                            }
                            else
                                return Request.CreateResponse(HttpStatusCode.NotFound, "user not found");

                        }
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, user);

        }
        public bool ActiveDirectoryAuthenticate(string username, string password)
        {
            bool result = false;
            using (DirectoryEntry _entry = new DirectoryEntry())
            {
                _entry.Username = username;
                _entry.Password = password;
                DirectorySearcher _searcher = new DirectorySearcher(_entry);
                _searcher.Filter = "(objectclass=user)";
                try
                {
                    SearchResult _sr = _searcher.FindOne();
                    string _name = _sr.Properties["displayname"][0].ToString();
                    result = true;
                }
                catch
                { /* Error handling omitted to keep code short: remember to handle exceptions !*/ }
            }

            return result; //true = user authenticated!
        }
        [HttpPost]
        public override void Create([FromBody]UserInfo entity)
        {
            if (entity != null)
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        if (!string.IsNullOrEmpty(entity.JobTitleID))
                        {
                            var JobTitleID = new Guid(entity.JobTitleID);
                            JobTitle JobTitle = Session.QueryOver<JobTitle>().Where(x => x.Id == JobTitleID).SingleOrDefault();
                            entity.JobTitle = JobTitle;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitID))
                        {
                            var OrganizationUnitID = new Guid(entity.OrganizationUnitID);
                            OrganizationUnit OrganizationUnit = Session.QueryOver<OrganizationUnit>().Where(x => x.Id == OrganizationUnitID).SingleOrDefault();
                            entity.OrganizationUnit = OrganizationUnit;
                        }
                        if (!string.IsNullOrEmpty(entity.ReportingToID))
                        {
                            var ReportingToID = new Guid(entity.ReportingToID);
                            UserInfo ReportingTo = Session.QueryOver<UserInfo>().Where(x => x.Id == ReportingToID).SingleOrDefault();
                            entity.ReportingTo = ReportingTo;
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
        public override HttpResponseMessage Update([FromUri]Guid id, [FromBody]UserInfo entity)
        {
            if (entity != null)
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {
                        if (!string.IsNullOrEmpty(entity.JobTitleID))
                        {
                            var JobTitleID = new Guid(entity.JobTitleID);
                            JobTitle JobTitle = Session.QueryOver<JobTitle>().Where(x => x.Id == JobTitleID).SingleOrDefault();
                            entity.JobTitle = JobTitle;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitID))
                        {
                            var OrganizationUnitID = new Guid(entity.OrganizationUnitID);
                            OrganizationUnit OrganizationUnit = Session.QueryOver<OrganizationUnit>().Where(x => x.Id == OrganizationUnitID).SingleOrDefault();
                            entity.OrganizationUnit = OrganizationUnit;
                        }
                        if (!string.IsNullOrEmpty(entity.ReportingToID))
                        {
                            var ReportingToID = new Guid(entity.ReportingToID);
                            UserInfo ReportingTo = Session.QueryOver<UserInfo>().Where(x => x.Id == ReportingToID).SingleOrDefault();
                            entity.ReportingTo = ReportingTo;
                        }

                        entity.LastModified = DateTime.Now;
                        entity.LastModifiedBy = Identity.UserID();

                        Session.Update(entity);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);

        }
        [HttpPut]
        public HttpResponseMessage UpdateProfile([FromUri]Guid id, [FromBody]UserInfo entity)
        {
            UserInfo orginaluser;
            if (entity != null)
            {

                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession Session = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = Session.BeginTransaction())
                    {

                        UserInfo user = Session.QueryOver<UserInfo>().Where(x => x.Id == id).SingleOrDefault();
                        orginaluser = user;

                        if (!string.IsNullOrEmpty(entity.JobTitleID))
                        {
                            var JobTitleID = new Guid(entity.JobTitleID);
                            JobTitle JobTitle = Session.QueryOver<JobTitle>().Where(x => x.Id == JobTitleID).SingleOrDefault();
                            orginaluser.JobTitle = JobTitle;
                        }
                        if (!string.IsNullOrEmpty(entity.OrganizationUnitID))
                        {
                            var OrganizationUnitID = new Guid(entity.OrganizationUnitID);
                            OrganizationUnit OrganizationUnit = Session.QueryOver<OrganizationUnit>().Where(x => x.Id == OrganizationUnitID).SingleOrDefault();
                            orginaluser.OrganizationUnit = OrganizationUnit;
                        }
                        if (!string.IsNullOrEmpty(entity.ReportingToID))
                        {
                            var ReportingToID = new Guid(entity.ReportingToID);
                            UserInfo ReportingTo = Session.QueryOver<UserInfo>().Where(x => x.Id == ReportingToID).SingleOrDefault();
                            orginaluser.ReportingTo = ReportingTo;
                        }
                        orginaluser.About = entity.About;
                        orginaluser.ArabicFullName = entity.ArabicFullName;
                        orginaluser.Password = entity.Password;
                        orginaluser.BirthDate = entity.BirthDate;
                        orginaluser.Mobile = entity.Mobile;
                        orginaluser.Email = entity.Email;
                        orginaluser.Photo = entity.Photo;
                        orginaluser.Status = entity.Status;

                        orginaluser.LastModified = DateTime.Now;
                        orginaluser.LastModifiedBy = Identity.UserID();

                        Session.Update(orginaluser);
                        tx.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);

        }
        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            UserInfoRepository entityRepository = new UserInfoRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<UserInfo>()
           .Where
           (Restrictions.On<UserInfo>(x => x.ArabicFullName).IsLike("%" + keyword + "%") ||
           Restrictions.On<UserInfo>(x => x.About).IsLike("%" + keyword + "%") ||
           Restrictions.On<UserInfo>(x => x.Email).IsLike("%" + keyword + "%") ||
           Restrictions.On<UserInfo>(x => x.LatinFullName).IsLike("%" + keyword + "%"))
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
        public override HttpResponseMessage GetAll()
        {
            UserInfoRepository entityRepository = new UserInfoRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.GetAll();

            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].JobTitle != null)
                        entities[i].JobTitleID = entities[i].JobTitle.Id.ToString();

                    if (entities[i].OrganizationUnit != null)
                        entities[i].OrganizationUnitID = entities[i].OrganizationUnit.Id.ToString();

                    if (entities[i].ReportingTo != null)
                        entities[i].ReportingToID = entities[i].ReportingTo.Id.ToString();
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
