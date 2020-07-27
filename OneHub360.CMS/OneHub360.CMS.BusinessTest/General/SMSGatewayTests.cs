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
    public class SMSGatewayTests
    {
        [TestMethod()]
        public void SendSMSTest()
        {
            var smsGateway = new SMSGateway();
            //smsGateway.SetUnicode(false);
            //smsGateway.SendSMS("تم مشاركة مراسلة معك");
            var result = smsGateway.SendUnicodeSMS("تم مشاركة مسودة معك", "96590084822" );

            Console.WriteLine(result);

        }
    }
}