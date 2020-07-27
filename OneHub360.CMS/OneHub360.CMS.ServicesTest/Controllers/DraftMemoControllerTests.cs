using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Services.Controllers.Tests
{
    [TestClass()]
    public class DraftMemoControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var draftMemo = new DraftMemo
            {
                CreatedBy = "ashok",
                Subject = "Test",
                From = "From 1",
                To = "To 1",
                Status = DraftMemoStatus.New,
                Language = DB.Language.AR
            };

            var draftMemoController = new DraftMemoController();

            draftMemoController.Create(draftMemo);
        }
    }
}