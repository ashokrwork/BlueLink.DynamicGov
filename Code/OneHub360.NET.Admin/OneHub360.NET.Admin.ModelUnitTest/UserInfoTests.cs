using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class UserInfoTests
    {
        [TestMethod]
        public void InsertUserInfo()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            UserInfo userInfo = new UserInfo {
                CreatedBy = "mameen",
                About = "About Magdy Ameen",
                ArabicFullName="مجدي محمد أمين",
                Email="mameen@mof.gov.kw",
                LatinFullName="Magdy Ameen",
                LoginName="mameen",
                Mobile="97884831"};

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    sess.Save(userInfo);
                    tx.Commit();
                }
            }
        }
    }
}
