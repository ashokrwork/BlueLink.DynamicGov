using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.Tests
{
    [TestClass()]
    public class DocumentRepositoryTests
    {
        [TestMethod()]
        public void DocumentRepositoryTest()
        {
            var context = new CMSContext(true);
            var repository = DocumentRepositoryFactory.GetInstance(context);

            var document = repository.GetById(Guid.Parse("C63086E7-6F3B-4C50-B3DA-55BB97A5B144"));

            document.FileBinary = File.ReadAllBytes(@"c:/test/test.docx");

            repository.UpdateDocument(document);

            context.Transaction.Commit();
            context.Dispose();
        }

        [TestMethod()]
        public void GetFileNetContentTest()
        {
            //var documentRepository = new DocumentRepository(new CMSContext());

            //var documentBinary = documentRepository.GetFileNetContent(Guid.Parse("{C280879D-A0F2-4020-85D0-A31E9B27BA35}"));
        }

        [TestMethod()]
        public void CreateFileNetContentTest()
        {
            //var documentRepository = new DocumentRepository(new CMSContext());

            //var document = documentRepository.GetById(Guid.Parse("F2D2A260-A4A4-4620-B12D-8544EA1C50C5"));

            //var fileNetDocument = documentRepository.CreateFileNetContent(document);
        }
    }
}