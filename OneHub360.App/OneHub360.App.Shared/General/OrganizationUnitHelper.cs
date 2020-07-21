using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace OneHub360.App.Shared.General
{
    public class OrganizationUnitHelper :AdminContext
    {
        public string[] GetSerialParts(string userID)
        {

            //Query over roles
            var query = "SELECT OrganizationUnit.Prifix, OrganizationUnit.LastGeneratedNumber " +
                            " FROM            OrganizationUnit INNER JOIN " +
                         " UserInfo ON OrganizationUnit.Id = UserInfo.FK_OrganizationUnit" +
                        " where UserInfo.Id = :userid ";
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
                            .SetString("userid", userID)
                            .UniqueResult();
            if (queryresult != null)
            {
                return new string[] { (string)((object[])queryresult)[0], ((int)(((object[])queryresult)[1])+1).ToString() };
            }
            return new string[] { "", "" };
        }
        public bool updateLastNumber(string userID)
        {

            //Query over roles
            var query = "update OrganizationUnit set LastGeneratedNumber=LastGeneratedNumber+1 where id in( SELECT OrganizationUnit.ID " +
                            " FROM            OrganizationUnit INNER JOIN " +
                         " UserInfo ON OrganizationUnit.Id = UserInfo.FK_OrganizationUnit" +
                        " where UserInfo.Id = :userid )";
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
                            .SetString("userid", userID)
                            .ExecuteUpdate();
            if (queryresult > 0)
            {
                return true;
            }
            return false;
        }
    }
}
