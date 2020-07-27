using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.Tests
{
    [TestClass()]
    public class IncomingMemoRepositoryTests
    {
        [TestMethod()]
        public void IncomingMemoRepositoryTest()
        {
            

            //var incomingMemoRepository = new IncomingMemoRepository();
            //var incomingMemo = new IncomingMemo
            //{
            //    CreatedBy = "ashokr",
            //    Subject = "Test",
            //    From = "From 1",
            //    To = "To 1",
            //    Document = new Document { Id = Guid.Parse("66555BD9-C962-44D0-A9AE-B19A2A2AAEB2") },
            //    IncomingDate = DateTime.Now,
            //    IncomingNumber = "1234"
            //};

            //incomingMemoRepository.Insert(incomingMemo);

            //var incomingMemos = incomingMemoRepository.GetAll();
        }

        [TestMethod()]
        public void SearchTest()
        {
            using (var context = new CMSContext())
            {
                var query = context.Session.GetNamedQuery("Search");

                query.SetParameter("keyword", "وزارة");
                query.SetParameter("user", "a65b95f3-c45d-47a9-9ae9-1256ad598eba");

                var result = query.List<KeywordSearchResult>();
            }
        }
    }
}