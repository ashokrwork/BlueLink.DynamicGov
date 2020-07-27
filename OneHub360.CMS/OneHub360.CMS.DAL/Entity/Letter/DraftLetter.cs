using System;
using NHibernate.Validator.Constraints;


namespace OneHub360.CMS.DAL
{

    public partial class DraftLetter : Correspondence
    {
        public virtual string PersonTitle { get; set; }
        [NotNullNotEmpty]
        public virtual string IncomingNumber { get; set; }
        public virtual DateTime? IncomingDate { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
       
        [NotNullNotEmpty]
        public virtual Guid FK_ReplyTo { get; set; }

       

        protected internal virtual CorrespondenceType CorrespondenceType { get { return CorrespondenceType.DraftLetter; } }

    }
}
