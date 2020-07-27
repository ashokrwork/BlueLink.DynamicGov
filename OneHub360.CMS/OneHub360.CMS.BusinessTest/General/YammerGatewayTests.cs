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
    public class YammerGatewayTests
    {
        [TestMethod()]
        public void SendMessageTest()
        {
            YammerGateway.SendMessage("Test from unit testing", 1606622398);
        }

        [TestMethod()]
        public void GetUserMessagesTest()
        {
            var messages = YammerGateway.GetUserMessages();
        }
    }
}