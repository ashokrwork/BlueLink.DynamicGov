using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void TestGroup()
        {
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();
            
                    var groupRes = new GroupRepository();
            groupRes.Session = sessionFactory.OpenSession();
                    groupRes.GetAll();


                
        }
        //[TestMethod]
        //public void InsertInternalGroup()
        //{
        //    // Initialize session
        //    var sessionFactory = new Configuration().Configure().BuildSessionFactory();

        //    InternalGroup internalGroup = new InternalGroup { CreatedBy = "mameen", Name = "مجموعة داخلية",Status=GroupStatus.Inactive };

        //    using (ISession sess = sessionFactory.OpenSession())
        //    {
        //        using (ITransaction tx = sess.BeginTransaction())
        //        {
        //            UserInfo user = sess.Get<UserInfo>(new Guid("902980e4-32c5-4dad-9fd9-a6a9015c8755"));
        //            internalGroup.OwnerId = user.Id;
        //            sess.Save(internalGroup);
        //            tx.Commit();
        //        }
        //    }
        //}

        //[TestMethod]
        //public void InsertExternalGroup()
        //{
        //    // Initialize session
        //    var sessionFactory = new Configuration().Configure().BuildSessionFactory();

        //    ExternalGroup externalGroup = new ExternalGroup { CreatedBy = "22222", Name = "مجموعة خارجية", Status = GroupStatus.Inactive };

        //    using (ISession sess = sessionFactory.OpenSession())
        //    {
        //        using (ITransaction tx = sess.BeginTransaction())
        //        {
        //            UserInfo user = sess.Get<UserInfo>(new Guid("902980e4-32c5-4dad-9fd9-a6a9015c8755"));
        //            externalGroup.OwnerId = user.Id;
        //            sess.SaveOrUpdate(externalGroup);
        //            tx.Commit();
        //        }
        //    }
        //}
    }
}
