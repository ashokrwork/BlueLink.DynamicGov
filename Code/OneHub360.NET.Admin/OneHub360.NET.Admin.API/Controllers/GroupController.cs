using NHibernate.Criterion;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Net.Http;
using System.Web.Http;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class GroupController : BaseController<Group, GroupRepository>
    {
        [HttpGet]
        public HttpResponseMessage Filter([FromUri]string keyword)
        {
            GroupRepository entityRepository = new GroupRepository();
            entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
            var entities = entityRepository.Session.QueryOver<Group>()
           .Where
           (Restrictions.On<Group>(x => x.Name).IsLike("%" + keyword + "%") )
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
        public virtual void adduser(string userid,string groupid)
        {
            if (!string.IsNullOrEmpty(userid))
            {
                UsersGroup UG = new UsersGroup();
                UsersGroupRepository entityRepository = new UsersGroupRepository();
                entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
                UG.FK_UserInfo = new Guid( userid);
                UG.Fk_Groups = new Guid(groupid);
                UG.CreationDate = DateTime.Now;
                UG.LastModified = DateTime.Now;
                UG.LastModifiedBy = Identity.UserID();
                UG.CreatedBy = Identity.UserID();
                entityRepository.Insert(UG);
            }
        }
        [HttpDelete]
        public void DeleteUser(string userid, string groupid)
        {
            if (!string.IsNullOrEmpty(userid))
            {
                
                UsersGroupRepository entityRepository = new UsersGroupRepository();
                entityRepository.Session = WebApiApplication.SessionFactory.GetCurrentSession();
                UsersGroup UG = entityRepository.Session.QueryOver<UsersGroup>().Where(x => x.FK_UserInfo == new Guid(userid)).Where(x => x.Fk_Groups == new Guid(groupid)).SingleOrDefault();
                entityRepository.Delete(UG);
                entityRepository.Session.Flush();
            }
        }
    }
}
