using NHibernate.Validator.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OneHub360.CMS.DAL
{
    public partial class DocumentsView : DB.NHEntity
    {
        
        public virtual System.Guid Id { get; set; }
        
        public virtual string CreatedBy { get; set; }
        
        public virtual DateTime CreationDate { get; set; }
        
        public virtual DateTime LastModified { get; set; }
        public virtual bool? IsDeleted { get; set; }
        public virtual int? Language { get; set; }
        
        public virtual System.Guid EntityId { get; set; }
        
        public virtual string FileName { get; set; }
        
        public virtual string Title { get; set; }
        
        public virtual bool Signed { get; set; }
        public virtual string SignedBy { get; set; }
        public virtual DateTime? SigningDate { get; set; }
        public virtual System.Nullable<System.Guid> FK_Template { get; set; }
        public virtual System.Nullable<System.Guid> FK_Correspondence { get; set; }
        public virtual System.Nullable<System.Guid> FK_Document { get; set; }

        public virtual string FileNetDocumentID { get; set; }

        public virtual string ViewUrl
        {
            get
            {
                

                var fileExtention = Path.GetExtension(FileName).ToLower();
                var fileUrl = string.Empty;
                switch (fileExtention)
                {
                    case ".pdf":
                        fileUrl = string.Format("{0}?file={1}cms/document/file/{2}", ModuleConstants.PdfViewUrl, CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document);
                        break;
                    case ".doc":
                    case ".docx":
                        fileUrl = string.Format("{0}{1}", CMSConfigLoader.Generator.configData.WordViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    case ".xls":
                    case ".xlsx":
                        fileUrl = string.Format("{0}{1}", CMSConfigLoader.Generator.configData.ExcelViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.xlsx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    case ".ppt":
                    case ".pptx":
                        fileUrl = string.Format("{0}{1}", CMSConfigLoader.Generator.configData.PowerPointViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.xlsx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    default:
                        fileUrl = string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document);
                           break;
                }
                return fileUrl;
            }
        }

        public virtual string PreviewUrl
        {
            get
            {


                var fileExtention = Path.GetExtension(FileName).ToLower();
                var fileUrl = string.Empty;
                switch (fileExtention)
                {
                    case ".pdf":
                        fileUrl = string.Format("{0}?PdfMode=1&WOPISrc={1}", CMSConfigLoader.Generator.configData.ImagePreviewUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    case ".doc":
                    case ".docx":
                        fileUrl = string.Format("{0}?WOPISrc={1}", CMSConfigLoader.Generator.configData.ImagePreviewUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    case ".xls":
                    case ".xlsx":
                        fileUrl = string.Format("{0}{1}", CMSConfigLoader.Generator.configData.ExcelViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.xlsx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    case ".ppt":
                    case ".pptx":
                        fileUrl = string.Format("{0}{1}", CMSConfigLoader.Generator.configData.PowerPointViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.xlsx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
                        break;
                    default:
                        fileUrl = string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document);
                        break;
                }
                return fileUrl;
            }
        }

        [NonSerialized()]
        private string[] ViewableFileExtentions  = new string[]
                {
                    ".pdf" , ".doc" , ".docx" , ".ppt" , ".pptx", ".xls" , ".xlsx" , ".jpg" , ".png",".tiff",".bmp",".jpeg"
                };

        public virtual bool IsWebViewable
        {
            get
            {
                
                var fileExtention = Path.GetExtension(FileName).ToLower();

                return ViewableFileExtentions.Contains(fileExtention);
            }
        }

        public virtual string ViewElement
        {
            get
            {
                var fileExtention = Path.GetExtension(FileName).ToLower();
                var viewElement = string.Empty;
                switch (fileExtention)
                {
                    case ".pdf":
                        viewElement = "iframe";
                        break;
                    case ".doc":
                    case ".docx":
                        viewElement = "iframe";
                        break;
                    case ".xls":
                    case ".xlsx":
                        viewElement = "iframe";
                        break;
                    case ".ppt":
                    case ".pptx":
                        viewElement = "iframe";
                        break;
                    case ".jpg":
                    case ".png":
                    case ".tiff":
                    case ".bmp":
                    case ".jpeg":
                        viewElement = "image";
                        break;
                    default:
                        viewElement = string.Empty;
                        break;
                }
                return viewElement;

            }
        }

        public virtual string FileIcon
        {
            get
            {
                var fileExtention = Path.GetExtension(FileName);
                var fileIcon = string.Empty;
                switch (fileExtention)
                {
                    case ".pdf":
                        fileIcon = "fa fa-file-pdf-o";
                        break;
                    case ".doc":
                    case ".docx":
                        fileIcon = "fa fa-file-word-o";
                        break;
                    case ".xls":
                    case ".xlsx":
                        fileIcon = "fa fa-file-excel-o";
                        break;
                    case ".ppt":
                    case ".pptx":
                        fileIcon = "fa fa-file-powerpoint-o";
                        break;
                    case ".jpg":
                    case ".png":
                        fileIcon = "fa fa-file-image-o";
                        break;
                    default:
                        fileIcon = "fa fa-file";
                        break;
                }
                return fileIcon;
            }
        }

        public virtual string EditUrl { get; }

        public virtual string PrintUrl
        {
            get
            {
                if (Guid.Empty == Id)
                    return string.Empty;

                var fileExtention = Path.GetExtension(FileName);
                var fileUrl = string.Empty;
                switch (fileExtention)
                {
                    case ".pdf":
                        fileUrl = string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, Id);
                        break;
                    case ".docx":
                        fileUrl = string.Format("{0}?WOPIsrc={1}&type=accesspdf&z={2}",
                            CMSConfigLoader.Generator.configData.PdfConvertionUrl, 
                            HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, Id)),
                            LastModified);
                        break;
                    default:
                        fileUrl = string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, Id);
                        break;
                }
                return fileUrl;
            }
        }
    }
}
