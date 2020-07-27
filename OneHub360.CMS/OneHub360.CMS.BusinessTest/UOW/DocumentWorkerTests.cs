using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.Business.UOW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.UOW.Tests
{
    [TestClass()]
    public class DocumentWorkerTests
    {

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod()]
        public void GetCorrespondenceDocumentsTest()
        {
                
            try
            {
                TestContext.WriteLine("Stagting");
                var correspondenceId = Guid.Parse("86b4eaf6-c78a-4c91-955d-519d473cc25c");

                var documentWorker = new DocumentWorker(OneHub360.Business.WorkerMode.NonQueue);

                var documents = documentWorker.GetDocument(correspondenceId);
            }
            catch(Exception ex)
            {
                TestContext.WriteLine(ex.Message);
            }
        }
    }
}