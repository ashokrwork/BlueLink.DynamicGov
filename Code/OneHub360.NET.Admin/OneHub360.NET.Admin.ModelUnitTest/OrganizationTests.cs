/*
 * This code belongs to Hewlett Packard Enterprise
 * Copyright © 2016 HPE -  All rights are reserved worldwide
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class OrganizationTests
    {
        [TestMethod]
        public void InsertOrganization()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            Organization organization = new Organization
            {
                CreatedBy = "mameen",
                About = "About Magdy Ameen",
                Email = "mameen@mof.gov.kw",
                IsLocal = false,
                Address = "address",
                Name = "Organization name should be here",
                Fax = "1232212"
            };

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    OrganizationType organizationType = sess.Get<OrganizationType>(new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"));
                    organization.OrganizationType = organizationType;

                    sess.Save(organization);
                    tx.Commit();
                }
            }
        }
    }
}
