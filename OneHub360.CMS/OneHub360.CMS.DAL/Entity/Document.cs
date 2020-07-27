using System;
using NHibernate.Validator.Constraints;
using FileNet.Api.Core;
using System.IO;

namespace OneHub360.CMS.DAL
{
    [Serializable]
    public partial class Document : DB.NHEntity {
        
        public virtual Guid FK_Template { get; set; }
        
        [NotNullNotEmpty]
        public virtual string FileName { get; set; }
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        [NotNullNotEmpty]
        public virtual byte[] FileBinary { get; set; }
        [NotNullNotEmpty]
        public virtual bool Signed { get; set; }
        public virtual string SignedBy { get; set; }
        public virtual DateTime? SigningDate { get; set; }
        public virtual long? PagesCount { get; set; }

        public virtual string FileNetDocumentID { get; set; }
    }
}
