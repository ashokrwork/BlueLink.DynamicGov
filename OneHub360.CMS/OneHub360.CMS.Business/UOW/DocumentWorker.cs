using System;
using System.Collections.Generic;
using System.IO;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using System.IO.Compression;
using System.Linq;
using OneHub360.DB;

namespace OneHub360.CMS.Business
{
    public class DocumentWorker : CMSWorkerBase
    {
        public DocumentWorker(WorkerMode mode) : base(mode)
        {
        }

        public void CreateEnvelop()
        {

        }

        public bool SaveDocumentFromWordOpenXml(Guid documentId,string wordOpenXml)
        {
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);

            var documentBinary = Convert.FromBase64String(wordOpenXml);

            documentRepository.SaveFileContent(documentId, documentBinary);

            return true;

        }

        public bool SaveTemplateFromWordOpenXml(Guid templateId, string wordOpenXml)
        {
            var templateRepository = new TemplateRepository(Context);

            var templateBinary = Convert.FromBase64String(wordOpenXml);

            var template = templateRepository.GetById(templateId);
            template.File = templateBinary;

            templateRepository.Update(template);

            return true;

        }

        public CheckFileInfoResponse GetFileInfo(Guid documentId)
        {
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            return documentRepository.GetFileInfo(documentId);
        }

        public byte[] GetFileContent(Guid documentId)
        {
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            return documentRepository.GetFileContent(documentId);
        }

        public byte[] GetTemplateContent(Guid templateId)
        {
            var templateRepository = new TemplateRepository(Context);
            return templateRepository.GetById(templateId).File;
        }

        public void SaveFileContent(Guid documentId,byte[] fileContent)
        {
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            documentRepository.SaveFileContent(documentId, fileContent);
        }

        public Document GetDocument(Guid documentId)
        {
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            return documentRepository.GetById(documentId);
        }

        public Template GetTemplate(Guid templateId)
        {
            var templateRepository = new TemplateRepository(Context);
            return templateRepository.GetById(templateId);
        }

    }
}
