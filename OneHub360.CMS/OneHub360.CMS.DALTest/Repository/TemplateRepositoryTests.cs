using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using OneHub360.CMS.DAL;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.Tests
{
    [TestClass()]
    public class TemplateRepositoryTests
    {
        [TestMethod()]
        public void TemplateRepositoryTest()
        {


            //var file = File.ReadAllBytes(@"C:\docs\MemoTemplate.pdf");

            //var templateRepository = new TemplateRepository(sessionFactory);

            //var template = templateRepository.Insert(new Template
            //{
            //    CreatedBy = @"ccmof\ashokr",
            //    File = file,
            //    Title = "Template 1.docx",
            //    Status = 1,
            //    Language = Language.AR
            //});

            //    var correspondenceRepository = new CorrespondenceRepository(sessionFactory);
            //    var correspondence = new Correspondence
            //    {
            //        CreatedBy = "ashok",
            //    Subject = "Test",
            //    From = "From 1",
            //    To = "To 1",
            //    Document = new Document { Id = Guid.Parse("66555BD9-C962-44D0-A9AE-B19A2A2AAEB2") }
            //};

            //    correspondenceRepository.Insert(correspondence);

            //var draftMemoRepository = new DraftMemoRepository();

            //var draftMemo = draftMemoRepository.GetById(Guid.Parse("57B0C9A9-226E-4740-AF3C-B98094788A67"));



            var draftMemo = new DraftMemo
            {
                CreatedBy = "ashok",
                Subject = "Test",
                From = "From 1",
                To = "To 1",
                FK_Document = Guid.Parse("66555BD9-C962-44D0-A9AE-B19A2A2AAEB2") 
            };


            //draftMemoRepository.Insert(draftMemo);

            //var correspondenceRepository = new CorrespondenceRepository(sessionFactory);

            //var correspondence = new Correspondence()
            //{
            //    CreatedBy = "ashokr",
            //    Subject = "Test",
            //    From = "From 1",
            //    To = "To 1",
            //    Document = new Document { Id= Guid.Parse("66555BD9-C962-44D0-A9AE-B19A2A2AAEB2") }
            //};

            // correspondenceRepository.Insert(correspondence);

        }

        [TestMethod()]
        public void UpdateTemplates()
        {
            var baseFolder = @"C:\inetpub\wwwroot\cms\OneHub-master\OneHub360.CMS\OneHub360.CMS.Business\Templates\";
            using (var context = new CMSContext(true))
            {
                var templateRepository = new TemplateRepository(context);
                var memoDocumentTemplate = templateRepository.GetById(Guid.Parse("153C9F52-C402-46B0-AC2A-F3D69812301C"));

                var memoFile = File.ReadAllBytes(string.Format("{0}{1}",baseFolder,"OneHub360MemoTemplate.docx"));

                memoDocumentTemplate.File = memoFile;
                memoDocumentTemplate.LastModified = DateTime.Now;

                templateRepository.Update(memoDocumentTemplate);

                var letterDocumentTemplate = templateRepository.GetById(Guid.Parse("E90FA041-883F-4F66-892F-A69001742444"));

                var letterFile = File.ReadAllBytes(string.Format("{0}{1}", baseFolder, "OneHub360LetterTemplate.docx"));

                letterDocumentTemplate.File = letterFile;
                letterDocumentTemplate.LastModified = DateTime.Now;

                templateRepository.Update(letterDocumentTemplate);

                context.Transaction.Commit();
            }
        }
    }
}