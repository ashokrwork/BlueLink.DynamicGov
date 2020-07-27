using NHibernate.Validator.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public partial class IncomingLetterView : DB.NHEntity
    {
        [NotNullNotEmpty]
        public virtual string Subject { get; set; }
        [NotNullNotEmpty]
        public virtual string From { get; set; }
        public virtual string To { get; set; }
        public virtual bool? Confidential { get; set; }
        public virtual string SharedWith { get; set; }
        public virtual Guid? FK_Parent { get; set; }
        public virtual Guid? ThreadId { get; set; }
        public virtual Guid? FK_Document { get; set; }
        public virtual string CopyTo { get; set; }
        public virtual string Brief { get; set; }
        //[NotNullNotEmpty]
        //public virtual string Organization { get; set; }
        public virtual string IncomingNumber { get; set; }
        public virtual DateTime? IncomingDate { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime RegisteringDate { get; set; }
        [NotNullNotEmpty]
        public virtual string RegisteredBy { get; set; }
        public virtual DateTime? IndexingDate { get; set; }
        public virtual string IndexedBy { get; set; }
        public virtual DateTime? ScanningDate { get; set; }
        public virtual string ScannedBy { get; set; }
        public virtual DateTime? SendingDate { get; set; }
        public virtual string SentBy { get; set; }
        public virtual string G2GNumber { get; set; }
        public virtual DateTime? G2GDate { get; set; }
        public virtual int? Status { get; set; }

        public virtual string MainDocumentViewUrl
        {
            get
            {
                return string.Format("{0}?file={1}", ModuleConstants.PdfViewUrl, string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, FK_Document));
            }
        }
    }
}
