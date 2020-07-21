using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class EntityTypeTests
    {
        [TestMethod]
        public void InsertOrganizationType()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            OrganizationType organizationType = new OrganizationType { CreatedBy = "mameen", Name = "وزارة" };

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    sess.Save(organizationType);
                    tx.Commit();
                }
            }
        }

        [TestMethod]
        public void InsertOrganizationUnitType()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            OrganizationUnitType organizationType = new OrganizationUnitType { CreatedBy = "mameen", Name = "إدارة" };

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    sess.Save(organizationType);
                    tx.Commit();
                }
            }
        }
    }
}
