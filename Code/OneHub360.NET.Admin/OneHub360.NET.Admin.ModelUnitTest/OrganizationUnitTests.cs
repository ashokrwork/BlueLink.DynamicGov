using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class OrganizationUnitTests
    {
        [TestMethod]
        public void InsertOrganizationUnit()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            OrganizationUnit organizationUnit = new OrganizationUnit
            {
                CreatedBy = "mameen",
                About = "About Magdy Ameen",
                Location = "Dubai",
                Title = "Organization Unit Name"
            };

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    UserInfo user = sess.Get<UserInfo>(new Guid("902980e4-32c5-4dad-9fd9-a6a9015c8755"));
                    organizationUnit.ManagerId = user.Id;


                    OrganizationUnitType organizationUnitType = sess.Get<OrganizationUnitType>(new Guid("ef008ec4-cc0a-4a5e-87de-a6a9015812c6"));
                    organizationUnit.OrganizationUnitType = organizationUnitType;
                   
                        sess.Save(organizationUnit);
                    tx.Commit();
                }
            }
        }
    }
}
