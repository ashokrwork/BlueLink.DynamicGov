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
    public class NumberGeneratorTests
    {
        [TestMethod()]
        public void TestTest()
        {
            var memoNumber = NumberGenerator.Generator.MemoIncomingNumber;
        }
    }
}