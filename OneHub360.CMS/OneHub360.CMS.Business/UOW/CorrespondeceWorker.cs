using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using OneHub360.DB;
using System.IO;
using System.IO.Compression;
using iTextSharp.text.pdf;
using NHibernate.Transform;

namespace OneHub360.CMS.Business
{
    public class CorrespondeceWorker : CMSWorkerBase
    {
        public CorrespondeceWorker(WorkerMode mode) : base(mode)
        {
        }

        

        public IList<Correspondence> KeyWordSearch(string keyword,Guid userId)
        {
            var query = Context.Session.GetNamedQuery("Search");

            query.SetParameter("keyword", keyword);
            query.SetParameter("user", userId);

            query.SetResultTransformer(
                        Transformers.AliasToBean(typeof(Correspondence)));


            return query.List<Correspondence>();
        }

        public CorrespondenceDocuments AddDocument(CreationInfo creationInfo,Guid correspondenceId,Guid documentId,CMSContext context)
        {
            var correspondenceDocumentsRepository = new CorrespondenceDocumentsRepository(context);

            var correspondenceDocument = correspondenceDocumentsRepository.InitEntity(creationInfo);
            correspondenceDocument.FK_Correspondence = correspondenceId;
            correspondenceDocument.FK_Document = documentId;

            correspondenceDocument = correspondenceDocumentsRepository.Insert(correspondenceDocument);
            return correspondenceDocument;
        }

        public IList<DocumentsView> GetAllDocuments(Guid correspondenceId)
        {
            long totalCount;

            IList<DocumentsView> documents;

            var documentsViewRepository = new DocumentsViewRepository(Context);
                
                    documents = documentsViewRepository.GetPaged(string.Format("FK_Correspondence = '{0}'",correspondenceId), "CreationDate", 0, int.MaxValue, out totalCount);
            
            return documents;
        }

        public IList<DocumentsView> GetSelectedDocuments(Guid correspondenceId,Guid[] selectedDocuments)
        {
            IList<DocumentsView> documents;

            documents = Context.Session.QueryOver<DocumentsView>().Where(D => D.FK_Correspondence == correspondenceId).AndRestrictionOn(D => D.Id).IsIn(selectedDocuments).List();

            return documents;
        }

        public byte[] GetCorrespondenceForPrinting(Guid correspondenceId)
        {
            var correspondenceDocuments = GetAllFiles(correspondenceId);
            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        document.Open();

                        for (int i = 0; i < correspondenceDocuments.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(correspondenceDocuments[i].FileBinary);
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n;)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                            }
                        }
                    }
                }
                mergedPdf = ms.ToArray();
            }

            return mergedPdf;
        }

        public IList<Document> GetAllFiles(Guid correspondenceId)
        {
            long totalCount;

            IList<Document> documents;

            var correspondenceDocumentsRepository = new CorrespondenceDocumentsRepository(Context);

            var correspondenceDocuments = correspondenceDocumentsRepository.GetPaged(string.Format("FK_Correspondence = '{0}'", correspondenceId), "CreationDate", 0, int.MaxValue, out totalCount);

            var documentIds = correspondenceDocuments.Select(P => P.FK_Document).ToArray();

            var documentsRepository = DocumentRepositoryFactory.GetInstance(Context);

            documents = documentsRepository.QueryOverIn<Document, Guid>(P => P.Id, documentIds);

            return documents;
        }

        public IList<DocumentsView> GetAttachements(Guid correspondenceId)
        {
            long totalCount;

            IList<DocumentsView> documents;

            var documentsViewRepository = new DocumentsViewRepository(Context);
                
                    documents = documentsViewRepository.GetPaged(string.Format("FK_Correspondence = '{0}' and Id <> '{0}'", correspondenceId), "CreationDate desc", 0, int.MaxValue, out totalCount);
                
            return documents;
        }

        public IList<DocumentsView> GetAttachements(Guid correspondenceId,string[] selectedAttachements)
        {
            long totalCount;

            IList<DocumentsView> documents;

            var documentsViewRepository = new DocumentsViewRepository(Context);

            documents = documentsViewRepository.GetPaged(string.Format("FK_Correspondence = '{0}' and Id <> '{0}'", correspondenceId), "CreationDate", 0, int.MaxValue, out totalCount);

            return documents;
        }

        public DocumentsView GetMainDocument(Guid correspondenceId)
        {
            DocumentsView mainDocument;
            long totalCount;
            var documentsViewRepository = new DocumentsViewRepository(Context);

            mainDocument = documentsViewRepository.GetPaged(string.Format("FK_Correspondence = '{0}' and Id = '{0}'", correspondenceId), "", 0, int.MaxValue, out totalCount).FirstOrDefault();
                
            
            return mainDocument;
        }

        public void AddMainDocument(Guid correspondenceId,Document document,bool makeCopy)
        {
            var correspondenceDocumentRepository = new CorrespondenceDocumentsRepository(Context);
            if (makeCopy)
            {
                var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
                document.Id = correspondenceId;
                if (documentRepository.GetById(document.Id) == null)
                {
                   
                    documentRepository.InsertDocument(document);
                    var correspondenceDocument = correspondenceDocumentRepository.CreateFromDocument(correspondenceId, document);
                    correspondenceDocument.Id = correspondenceId;
                    correspondenceDocumentRepository.Insert(correspondenceDocument);
                }
                else
                {
                    documentRepository.ForceDelete(document.Id);
                   
                    documentRepository.InsertDocument(document);
                }
            }

            
        }
        public void updateMainDocument(Guid correspondenceId, Document document, bool makeCopy)
        {
            var correspondenceDocumentRepository = new CorrespondenceDocumentsRepository(Context);
            if (makeCopy)
            {
                var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
                document.Id = correspondenceId;
                documentRepository.Update(document);
            }
            var correspondenceDocument = correspondenceDocumentRepository.CreateFromDocument(correspondenceId, document);
            correspondenceDocument.Id = correspondenceId;
            correspondenceDocumentRepository.Update(correspondenceDocument);
        }

        public void AddAttachement(Guid correspondenceId, Document document)
        {
            var correspondenceDocumentRepository = new CorrespondenceDocumentsRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            documentRepository.InsertDocument(document);

            var correspondenceDocument = correspondenceDocumentRepository.CreateFromDocument(correspondenceId, document);
            correspondenceDocumentRepository.Insert(correspondenceDocument);
        }

        public void CopyCorrespondenceAttachements(CreationInfo creationInfo,Guid sourceCorrespondence,Guid destinationCorrespondence,bool makeCopy)
        {
            var sourceAttachements = GetAttachements(sourceCorrespondence);

            var correspondenceDocumentsRepository = new CorrespondenceDocumentsRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            foreach (var attachement in sourceAttachements)
            {
                if (makeCopy)
                {
                    var document = documentRepository.GetById(attachement.FK_Document.Value);
                    document = documentRepository.InitEntity(document, creationInfo);
                    documentRepository.Insert(document);

                    var correspondenceDocument = correspondenceDocumentsRepository.InitEntity(creationInfo);
                    correspondenceDocument.FK_Correspondence = destinationCorrespondence;
                    correspondenceDocument.FK_Document = document.Id;
                    correspondenceDocumentsRepository.Insert(correspondenceDocument);
                }
                else
                {
                    var correspondenceDocument = correspondenceDocumentsRepository.InitEntity(creationInfo);
                    correspondenceDocument.FK_Correspondence = destinationCorrespondence;
                    correspondenceDocument.FK_Document = attachement.FK_Document.Value;
                    correspondenceDocumentsRepository.Insert(correspondenceDocument);
                }
            }
        }

        public void CopyCorrespondenceAttachements(CreationInfo creationInfo, IList<DocumentsView> sourceAttachements, Guid destinationCorrespondence, bool makeCopy)
        {
            var correspondenceDocumentsRepository = new CorrespondenceDocumentsRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            foreach (var attachement in sourceAttachements)
            {
                if (makeCopy)
                {
                    var document = documentRepository.GetById(attachement.FK_Document.Value);
                    document = documentRepository.InitEntity(document, creationInfo);
                    documentRepository.Insert(document);

                    var correspondenceDocument = correspondenceDocumentsRepository.InitEntity(creationInfo);
                    correspondenceDocument.FK_Correspondence = destinationCorrespondence;
                    correspondenceDocument.FK_Document = document.Id;
                    correspondenceDocumentsRepository.Insert(correspondenceDocument);
                }
                else
                {
                    var correspondenceDocument = correspondenceDocumentsRepository.InitEntity(creationInfo);
                    correspondenceDocument.FK_Correspondence = destinationCorrespondence;
                    correspondenceDocument.FK_Document = attachement.FK_Document.Value;
                    correspondenceDocumentsRepository.Insert(correspondenceDocument);
                }
            }
        }

        public byte[] GetCorrespondenceZip(Guid correspondenceId)
        {
            var documents = GetAllFiles(correspondenceId);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            return documentRepository.ZipDocuments(documents);
        }

    }
}
