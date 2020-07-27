using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.Business;
using OneHub360.CMS.Business.UOW;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.UOW.Tests
{
    [TestClass()]
    public class DraftMemoWorkerTests
    {
        [TestMethod()]
        public void CreateDraftTest()
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

            var DraftMemoWorker = new DraftMemoWorker(WorkerMode.NonQueue);

            DraftMemoWorker.ProcessCreate(draftMemo);


            var workerBase = new DraftMemoWorker(WorkerMode.NonQueue);
            //var message = workerBase.RecieveWorkMessage();

            //message.Process();

        }

        [TestMethod()]
        public void RecieveTest()
        {
            //var workerBase = new WorkerBase();
            //var message = workerBase.RecieveWorkMessage();

            //message.Process();
        }
    }
}