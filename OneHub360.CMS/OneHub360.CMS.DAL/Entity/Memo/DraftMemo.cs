using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.CMS.DAL
{
    /// <summary>
    /// Draft memo class
    /// Feed mapping:
    /// Number1 = Number of attachements (From Documents where FK_Correspondence = Id)
    /// Text1 = OutgoingNumber
    /// Date1 = OutgoingDate
    /// </summary>
    [Serializable]
    public partial class DraftMemo : Correspondence
    {
        public virtual string IncomingNumber { get; set; }
        public virtual DateTime? IncomingDate { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        [NotNullNotEmpty]
        
        protected internal virtual CorrespondenceType CorrespondenceType { get { return CorrespondenceType.DraftMemo; } }

        
    }
}
