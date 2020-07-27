using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.Tests
{
    [TestClass()]
    public class CorrespondeceWorkerTests
    {
        [TestMethod()]
        public void KeyWordSearchTest()
        {
            var correspondenceWorker = new CorrespondeceWorker(OneHub360.Business.WorkerMode.NonQueue);

            var result = correspondenceWorker.KeyWordSearch("كتاب", Guid.Parse("e0050903-2b1a-4dbb-a5ac-0015f36c0f25"));
        }
    }
}