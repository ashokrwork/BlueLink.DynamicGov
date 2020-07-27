using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using System.Runtime.Serialization;

namespace OneHub360.CMS.DAL
{

    public partial class IncomingLetter : Correspondence
    {

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
      
        public virtual string RejectedReason { get; set; }

        protected internal virtual CorrespondenceType CorrespondenceType { get { return CorrespondenceType.IncomingLetter; } }
    }
}
