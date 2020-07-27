using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.DAL.DigitalSignature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.DigitalSignature.Tests
{
    [TestClass()]
    public class FileSignManagerTests
    {
        [TestMethod()]
        public void SignWordDocumentTest()
        {
            X509Certificate2 cer = new X509Certificate2();
            cer.Import(@"C:\Test\certificate.pfx","1234",X509KeyStorageFlags.Exportable);

            

            FileSignManager.SignWordDocument(@"C:\Test\Test.docx","لإجراء اللازم", @"C:\Test\signature1.png", cer);
            FileSignManager.SignWordDocument(@"C:\Test\Test.docx","لإبداء الرأي", @"C:\Test\signature2.png", cer);


        }
    }
}