using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using OneHub360.NET.Admin.Model;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass]
    public class InvitationTests
    {
        [TestMethod]
        public void InsertInvitation()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            Invitation invitation = new Invitation
            {
                CreatedBy = "mameen",
                ReplayDate = DateTime.Now,
                Status = InvitationStatus.Sent
            };

           

            using (ISession sess = sessionFactory.OpenSession())
            {
                using (ITransaction tx = sess.BeginTransaction())
                {
                    UserInfo user = sess.Get<UserInfo>(new Guid("902980e4-32c5-4dad-9fd9-a6a9015c8755"));
                    invitation.UserInfoFrom = user;
                    invitation.UserInfoTo = user;

                    sess.Save(invitation);
                    tx.Commit();
                }
            }
        }
    }
}
