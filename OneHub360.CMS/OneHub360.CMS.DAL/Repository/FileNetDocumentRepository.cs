using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.DB;
using FileNet.Api.Core;
using Microsoft.Web.Services3.Security.Tokens;
using System.IO;
using FileNet.Api.Constants;
using System.Web;

namespace OneHub360.CMS.DAL
{
    public class FileNetDocumentRepository : DocumentRepositoryFactory
    {
        public FileNetDocumentRepository(IDBContext context) : base(context)
        {

        }

        public override Document InsertDocument(Document entity)
        {
            var fileNetDocument = CreateFileNetContent(entity);
            entity.FileNetDocumentID = fileNetDocument.Id.ToString();

            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.Entity = entity;
            OnEntityInserting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return entity;

            entity.Id = (Guid)Context.Session.Save(entity);

            // Initialize the NHEntityEventArgs and fire the EntityInserted Event
            entityEventArgs.Entity = entity;
            OnEntityInserted(entityEventArgs);

            return entity;
        }

        private IDocument CreateFileNetContent(Document document)
        {
            var token = new UsernameToken(ModuleConstants.FileNet.Username, ModuleConstants.FileNet.Password, PasswordOption.SendPlainText);
            FileNet.Api.Util.UserContext.SetThreadSecurityToken(token);

            var fileNetConnection = Factory.Connection.GetConnection(ModuleConstants.FileNet.Connection);
            var fileNetDomain = Factory.Domain.GetInstance(fileNetConnection, ModuleConstants.FileNet.Domain);
            var fileNetObjectStore = Factory.ObjectStore.GetInstance(fileNetDomain, ModuleConstants.FileNet.ObjectStoreName);
            var fileNetDocument = Factory.Document.CreateInstance(fileNetObjectStore, ModuleConstants.FileNet.CalssId);

            fileNetDocument.Properties["DocumentTitle"] = document.Title;
            fileNetDocument.MimeType = GetMimeType(Path.GetExtension(document.FileName));

            var fileNetContentElementList = Factory.ContentElement.CreateList();

            var fileNetcontentTransfer = Factory.ContentTransfer.CreateInstance();

            var fileStream = new MemoryStream(document.FileBinary);
            fileNetcontentTransfer.SetCaptureSource(fileStream);
            fileNetcontentTransfer.RetrievalName = document.Title;

            fileNetContentElementList.Add(fileNetcontentTransfer);

            fileNetDocument.ContentElements = fileNetContentElementList;

            fileNetDocument.Save(RefreshMode.REFRESH);

            IFolder fileNetFolder = Factory.Folder.FetchInstance(fileNetObjectStore, ModuleConstants.FileNet.Folder, null);
            IReferentialContainmentRelationship rcr = null;

            rcr = fileNetFolder.File(fileNetDocument, AutoUniqueName.AUTO_UNIQUE, fileNetDocument.Name, DefineSecurityParentage.DO_NOT_DEFINE_SECURITY_PARENTAGE);
            rcr.Save(RefreshMode.REFRESH);

            fileNetDocument.Checkin(AutoClassify.AUTO_CLASSIFY, CheckinType.MAJOR_VERSION);

            fileNetDocument.Save(RefreshMode.REFRESH);

            return fileNetDocument;
        }
        
        private string GetMimeType(string fileExtention)
        {
            var mimeType = string.Empty;
            switch (fileExtention)
            {
                case ".pdf":
                    mimeType = "application/pdf";
                    break;
                default:
                    mimeType = fileExtention;
                    break;
            }

            return mimeType;
        }

        public override byte[] GetFileContent(Guid documentId)
        {
            var document = GetById(documentId);

            byte[] result;
            var token = new UsernameToken(ModuleConstants.FileNet.Username, ModuleConstants.FileNet.Password, PasswordOption.SendPlainText);
            FileNet.Api.Util.UserContext.SetThreadSecurityToken(token);

            var fileNetConnection = Factory.Connection.GetConnection(ModuleConstants.FileNet.Connection);
            var fileNetDomain = Factory.Domain.GetInstance(fileNetConnection, ModuleConstants.FileNet.Domain);
            var fileNetObjectStore = Factory.ObjectStore.GetInstance(fileNetDomain, ModuleConstants.FileNet.ObjectStoreName);
            var fileNetDocument = Factory.Document.FetchInstance(fileNetObjectStore, document.FileNetDocumentID, null);

            var fileNetContentStream = ((IContentTransfer)fileNetDocument.ContentElements[0]).AccessContentStream();
            byte[] buffer = new byte[fileNetContentStream.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = fileNetContentStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                result = ms.ToArray();
            }

            return result;

        }

        public override CheckFileInfoResponse GetFileInfo(Guid documentId)
        {
            var checkFileInfoResponse = new CheckFileInfoResponse();

            var baseUrl = string.Format("{0}//{1}:{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);

            
            var document = GetById(documentId);

            var fileContent = GetFileContent(documentId);

            checkFileInfoResponse = new CheckFileInfoResponse
            {
                BaseFileName = document.FileName,
                LastModifiedTime = document.LastModified.ToString("O"),
                Version = document.LastModified.ToString("O"),
                OwnerId = document.CreatedBy,
                SupportsUpdate = true,
                UserCanNotWriteRelative = true, /* Because this host does not support PutRelativeFile */
                ReadOnly = false,
                UserCanWrite = true,
                AllowExternalMarketplace = true,
                SupportsScenarioLinks = true,
                RestrictedWebViewOnly = false,
                SupportsCoauth = true,
                SupportsCobalt = true,
                LicenseCheckForEditIsEnabled = true,
                FileExtension = ".docx",
                Size = fileContent.Length,
                ClientUrl = string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, documentId)
            };


            return checkFileInfoResponse;
        }

        public override void UpdateDocument(Document document)
        {
            var token = new UsernameToken(ModuleConstants.FileNet.Username, ModuleConstants.FileNet.Password, PasswordOption.SendPlainText);
            FileNet.Api.Util.UserContext.SetThreadSecurityToken(token);

            var fileNetConnection = Factory.Connection.GetConnection(ModuleConstants.FileNet.Connection);
            var fileNetDomain = Factory.Domain.GetInstance(fileNetConnection, ModuleConstants.FileNet.Domain);
            var fileNetObjectStore = Factory.ObjectStore.GetInstance(fileNetDomain, ModuleConstants.FileNet.ObjectStoreName);
            var fileNetDocument = Factory.Document.FetchInstance(fileNetObjectStore, new FileNet.Api.Util.Id(document.FileNetDocumentID),null);

            fileNetDocument.Properties["DocumentTitle"] = document.Title;
            fileNetDocument.MimeType = GetMimeType(Path.GetExtension(document.FileName));

            var fileNetContentElementList = Factory.ContentElement.CreateList();

            var fileNetcontentTransfer = Factory.ContentTransfer.CreateInstance();

            var fileStream = new MemoryStream(document.FileBinary);
            fileNetcontentTransfer.SetCaptureSource(fileStream);
            fileNetcontentTransfer.RetrievalName = document.Title;

            fileNetContentElementList.Add(fileNetcontentTransfer);

            fileNetDocument.ContentElements = fileNetContentElementList;

            fileNetDocument.Save(RefreshMode.REFRESH);

            IFolder fileNetFolder = Factory.Folder.FetchInstance(fileNetObjectStore, ModuleConstants.FileNet.Folder, null);
            IReferentialContainmentRelationship rcr = null;

            rcr = fileNetFolder.File(fileNetDocument, AutoUniqueName.AUTO_UNIQUE, fileNetDocument.Name, DefineSecurityParentage.DO_NOT_DEFINE_SECURITY_PARENTAGE);
            rcr.Save(RefreshMode.REFRESH);

            fileNetDocument.Checkin(AutoClassify.AUTO_CLASSIFY, CheckinType.MAJOR_VERSION);

            fileNetDocument.Save(RefreshMode.REFRESH);

            Update(document);
        }

        public override void SaveFileContent(Guid documentId, byte[] fileContent)
        {
            var document = GetById(documentId);

            var token = new UsernameToken(ModuleConstants.FileNet.Username, ModuleConstants.FileNet.Password, PasswordOption.SendPlainText);
            FileNet.Api.Util.UserContext.SetThreadSecurityToken(token);

            var fileNetConnection = Factory.Connection.GetConnection(ModuleConstants.FileNet.Connection);
            var fileNetDomain = Factory.Domain.GetInstance(fileNetConnection, ModuleConstants.FileNet.Domain);
            var fileNetObjectStore = Factory.ObjectStore.GetInstance(fileNetDomain, ModuleConstants.FileNet.ObjectStoreName);
            var fileNetDocument = Factory.Document.FetchInstance(fileNetObjectStore, new FileNet.Api.Util.Id(document.FileNetDocumentID), null);

            
            

            var fileNetContentElementList = Factory.ContentElement.CreateList();

            var fileNetcontentTransfer = Factory.ContentTransfer.CreateInstance();

            var fileStream = new MemoryStream(fileContent);
            fileNetcontentTransfer.SetCaptureSource(fileStream);
            

            fileNetContentElementList.Add(fileNetcontentTransfer);

            fileNetDocument.ContentElements = fileNetContentElementList;

            fileNetDocument.Save(RefreshMode.REFRESH);

            IFolder fileNetFolder = Factory.Folder.FetchInstance(fileNetObjectStore, ModuleConstants.FileNet.Folder, null);
            IReferentialContainmentRelationship rcr = null;

            rcr = fileNetFolder.File(fileNetDocument, AutoUniqueName.AUTO_UNIQUE, fileNetDocument.Name, DefineSecurityParentage.DO_NOT_DEFINE_SECURITY_PARENTAGE);
            rcr.Save(RefreshMode.REFRESH);

            fileNetDocument.Checkin(AutoClassify.AUTO_CLASSIFY, CheckinType.MAJOR_VERSION);

            fileNetDocument.Save(RefreshMode.REFRESH);

            
            document.LastModified = DateTime.Now;
            Update(document);
        }
    }
}
