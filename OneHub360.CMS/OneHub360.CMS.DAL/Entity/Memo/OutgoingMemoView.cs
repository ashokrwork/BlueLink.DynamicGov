﻿using NHibernate.Validator.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public  partial class OutgoingMemoView : DB.NHEntity
    {
        [NotNullNotEmpty]
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
        public virtual System.Nullable<System.Guid> FK_Parent { get; set; }
        public virtual System.Nullable<System.Guid> ThreadId { get; set; }
        public virtual System.Nullable<System.Guid> FK_Document { get; set; }
        public virtual string CopyTo { get; set; }
        public virtual int? CorrespondenceType { get; set; }
        public virtual string Brief { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_Draft { get; set; }
        public virtual System.Nullable<System.Guid> FK_Incoming { get; set; }
        public virtual int? Status { get; set; }

        public virtual string MainDocumentViewUrl
        {
            get
            {
                return string.Format("{0}?file={1}", ModuleConstants.PdfViewUrl, string.Format("{0}cms/document/file/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl,FK_Document));
            }
        }
    }
}