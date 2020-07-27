using System;
using NHibernate.Validator.Constraints;
using System.Web;

namespace OneHub360.CMS.DAL
{

    public partial class DraftMemoView : DB.NHEntity
    {
        public virtual System.Guid Id { get; set; }
        [NotNullNotEmpty]
        public virtual string CreatedBy { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime CreationDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastModified { get; set; }
        public virtual bool? IsDeleted { get; set; }
        public virtual int? Language { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid EntityId { get; set; }
        [NotNullNotEmpty]
        public virtual string Subject { get; set; }
        [NotNullNotEmpty]
        public virtual string From { get; set; }
        [NotNullNotEmpty]
        public virtual string To { get; set; }
        public virtual bool? Confidential { get; set; }
        public virtual string SharedWith { get; set; }
        public virtual Guid? ThreadId { get; set; }
        public virtual System.Nullable<System.Guid> FK_Parent { get; set; }
        public virtual System.Nullable<System.Guid> FK_Document { get; set; }
        public virtual string CopyTo { get; set; }
        public virtual string IncomingNumber { get; set; }
        public virtual DateTime? IncomingDate { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        [NotNullNotEmpty]
        public virtual int Status { get; set; }

        public virtual string MainDocumentViewUrl
        {
            get
            {
                
                return string.Format("{0}{1}", CMSConfigLoader.Generator.configData.WordViewerUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
            }
        }

        public virtual string MainDocumentPreviewUrl
        {
            get
            {
                return string.Format("{0}{1}", CMSConfigLoader.Generator.configData.FilePreviewUrl, HttpUtility.UrlEncode(string.Format("{0}/wopidb/files/{1}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document)));
            }
        }

        public virtual string AddtionalRecipients { get; set; }
    }
}
