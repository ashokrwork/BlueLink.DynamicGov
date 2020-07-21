using OneHub360.Authentication.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneHub360.Authentication.Entities;
using System.Threading.Tasks;
using OneHub360.App.DAL;
using OneHub360.App.Shared;
using NHibernate;
using NHibernate.Cfg;
using System.Web.Hosting;
using System.IO;
using OneHub360.NET.Admin.API.Controllers;

namespace OneHub360.App.Services.Manager
{
    public class OneHub360UserManager : IClaimsUserManager
    {
        public virtual CliamsUser Find(string username, string password)
        {


            if ((new LicenseController()).CheckLicense())
            {
                long totalCount;
                IList<UserInfos> result;
                string[] user_Usertype = new string[] { username, "2609FE8C-C597-4AC4-B751-A73D00A00C8D" };
                try
                {
                    user_Usertype = username.Split(new string[] { "#$%S$" }, StringSplitOptions.None);

                }
                catch
                {
                }
                using (var context = new AdminContext())
                {
                    var userInfoRepository = new UserInfoRepository(context);
                    //var usernameSplitted = username.Split('\\');
                    if (string.IsNullOrEmpty(password))
                        result = userInfoRepository.GetPaged(string.Format("ADUsername='{0}'", user_Usertype[0]), string.Empty, 0, 1, out totalCount);
                    else
                        result = userInfoRepository.GetPaged(string.Format("LoginName='{0}' and Password='{1}'", user_Usertype[0], password), string.Empty, 0, 1, out totalCount);
                }
                if (result.Count > 0)
                {
                    //Query over roles
                    var query = "SELECT [Role].[Name] " +
                             "FROM RolesUsers INNER JOIN " +
                             "[Role] ON RolesUsers.FK_Role = [Role].Id INNER JOIN " +
                             "UserInfo ON RolesUsers.FK_UserInfo = UserInfo.Id "
                    +
                    "WHERE(UserInfo.Id = '" + result[0].Id + "') " +
                    " AND([Role].Id = :roleid) ";
                    //if (!context.Session.IsOpen)
                    var configFilePath = string.Empty;

                    if (HostingEnvironment.ApplicationHost == null)
                        configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\app\\admin.config");
                    else
                        configFilePath = HostingEnvironment.MapPath("~/config/app/admin.config");
                    ISessionFactory sessionFactory;
                    sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();
                    var session = sessionFactory.OpenSession();
                    var queryresult = session.CreateSQLQuery(query)
                                    .SetString("roleid", user_Usertype[1])
                                    .List();
                    if (queryresult.Count == 0)
                        return new CliamsUser() { Id = "", UserName = "User do not have permission", Password = "" };
                    else
                        return new CliamsUser() { Id = result.First().Id.ToString(), UserName = username, Password = password };

                }
                else
                if (result.Count == 0)
                    return new CliamsUser() { Id = "", UserName = "User not found", Password = "" };
                //throw new Exception("User not found");

                // throw new Exception("User do not have permission");


            }
            else
            {
                return new CliamsUser() { Id = "", UserName = "Your licence is invalid, please contact Iknowledge for more info.", Password = "" };

            }
            return new CliamsUser() { Id = "", UserName = "Your licence is invalid, please contact Iknowledge for more info.", Password = "" };

        }


    }
}