using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.CMS.DAL
{

    public partial class IncomingMemo : Correspondence
    {

        
        [NotNullNotEmpty]
        public virtual string IncomingNumber { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime IncomingDate { get; set; }
        public virtual DateTime? ScanningDate { get; set; }
        public virtual string ScannedBy { get; set; }
        public virtual DateTime? IndexingDate { get; set; }
        public virtual string IndexedBy { get; set; }

        public virtual Guid FK_Draft { get; set; }

        public virtual Guid FK_Outgoing { get; set; }

        protected internal virtual CorrespondenceType CorrespondenceType { get { return CorrespondenceType.IncomingMemo; } }
    }
}
