using OneHub360.DB;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Linq;
using A = DocumentFormat.OpenXml.Drawing;
using System.Reflection;
using System;
using DocumentFormat.OpenXml;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;
using OneHub360.CMS.DAL.DigitalSignature;
using FileNet.Api.Core;
using Microsoft.Web.Services3.Security.Tokens;
using FileNet.Api.Collection;
using FileNet.Api.Constants;

namespace OneHub360.CMS.DAL
{
    public class SQLServerDocumentRepository : DocumentRepositoryFactory
    {
        public SQLServerDocumentRepository(IDBContext context) : base(context)
        {
        }

        public override byte[] GetFileContent(Guid documentId)
        {
            var document = GetById(documentId);
            return document.FileBinary;
        }



        public override CheckFileInfoResponse GetFileInfo(Guid documentId)
        {
            var checkFileInfoResponse = new CheckFileInfoResponse();

            var baseUrl = string.Format("{0}//{1}:{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);

            var document = GetById(documentId);

            checkFileInfoResponse = new CheckFileInfoResponse
            {
                BaseFileName = document.FileName,
                LastModifiedTime = document.LastModified.ToString("O"),
                Version = document.LastModified.ToString("O"),
                OwnerId = document.CreatedBy,
                SupportsUpdate = true,
                UserCanNotWriteRelative = false, /* Because this host does not support PutRelativeFile */
                ReadOnly = false,
                UserCanWrite = true,
                AllowExternalMarketplace = true,
                SupportsScenarioLinks = true,
                RestrictedWebViewOnly = false,
                SupportsCoauth = true,
                SupportsCobalt = true,
                FileExtension = ".docx",
                Size = document.FileBinary.Length,
                ProtectInClient = false
                //ClientUrl = CMSConfigLoader.Generator.configData.CMSServiceBaseUrl + "cms/document/file/download/" + document.Id + ".docx"
            };


            return checkFileInfoResponse;
        }

        public override Document InsertDocument(Document document)
        {
            if (Path.GetExtension(document.FileName).ToLower() == ".docx")
            {
                using (MemoryStream documentStream = new MemoryStream())
                {
                    documentStream.Write(document.FileBinary, 0, document.FileBinary.Length);

                    using (var wordDocument = WordprocessingDocument.Open(documentStream, true))
                    {
                        var customProps = wordDocument.CustomFilePropertiesPart;
                        if (customProps == null)
                        {
                            // No custom properties? Add the part, and the
                            // collection of properties now.
                            customProps = wordDocument.AddCustomFilePropertiesPart();
                            customProps.Properties =
                                new DocumentFormat.OpenXml.CustomProperties.Properties();
                        }

                        var newProp = new DocumentFormat.OpenXml.CustomProperties.CustomDocumentProperty();
                        newProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
                        newProp.VTLPWSTR = new DocumentFormat.OpenXml.VariantTypes.VTLPWSTR(document.Id.ToString());
                        newProp.Name = ModuleConstants.DocumentIdProperty;

                        var props = customProps.Properties;
                        if (props != null)
                        {
                            var prop =
                        props.Where(
                        p => ((DocumentFormat.OpenXml.CustomProperties.CustomDocumentProperty)p).Name.Value
                            == ModuleConstants.DocumentIdProperty).FirstOrDefault();

                            // Does the property exist? If so, get the return value, 
                            // and then delete the property.
                            if (prop != null)
                            {
                                prop.Remove();
                            }

                            // Append the new property, and 
                            // fix up all the property ID values. 
                            // The PropertyId value must start at 2.
                            props.AppendChild(newProp);
                            int pid = 2;
                            foreach (DocumentFormat.OpenXml.CustomProperties.CustomDocumentProperty item in props)
                            {
                                item.PropertyId = pid++;
                            }
                            props.Save();
                        }

                        wordDocument.Close();

                        using (var resultStream = new MemoryStream())
                        {
                            documentStream.WriteTo(resultStream);

                            document.FileBinary = resultStream.ToArray();
                        }
                    }
                }
            } 

            return Insert(document);
        }

        public override void UpdateDocument(Document document)
        {
            Update(document);
        }

        public override void SaveFileContent(Guid documentId, byte[] fileContent)
        {
            var document = GetById(documentId);
            document.LastModified = DateTime.Now;
            document.FileBinary = fileContent;
            Update(document);
        }
    }
}
