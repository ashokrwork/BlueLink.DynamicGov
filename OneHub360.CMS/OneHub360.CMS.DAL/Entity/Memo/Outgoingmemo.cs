using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.CMS.DAL {
    
    public partial class OutgoingMemo : Correspondence {

        
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        [NotNullNotEmpty]
        public virtual Guid FKDraft { get; set; }
        public virtual Guid? FKIncoming { get; set; }

        protected internal virtual CorrespondenceType CorrespondenceType { get { return CorrespondenceType.OutgoingMemo; } }

    }
}
