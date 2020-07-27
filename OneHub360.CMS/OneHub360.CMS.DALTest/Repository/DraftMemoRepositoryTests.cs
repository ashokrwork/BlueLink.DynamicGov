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
    public class DraftMemoRepositoryTests
    {
        [TestMethod()]
        public void DraftMemoRepositoryTest()
        {
            using (var context = new CMSContext())
            {
                var draftMemoRepository = new DraftMemoRepository(context);
                var draftMemo = new DraftMemo
                {
                    CreatedBy = "ashokr",
                    Subject = "Test",
                    From = "From 1",
                    To = "To 1",
                    Status = DraftMemoStatus.New
                };

                draftMemoRepository.Insert(draftMemo);
            }
        }
    }
}