using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void InsertRole()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            Role role = new Role
            {
                CreatedBy = "mameen",
                Name = "Admin"
            };

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    sess.Save(role);
                    tx.Commit();
                }
            }
        }
    }
}
