using System;
using NHibernate.Validator.Constraints;
using System.Web;

namespace OneHub360.CMS.DAL
{
    [Serializable]
    public class Correspondence : DB.NHEntity {
        

        public virtual Correspondence Parent { get; set; }
        public virtual Guid FK_Document { get; set; }
       
        [NotNullNotEmpty]
        public virtual string Subject { get; set; }
        [NotNullNotEmpty]
        public virtual string From { get; set; }
        
        public virtual string To { get; set; }
        public virtual bool? Confidential { get; set; }
        public virtual string SharedWith { get; set; }
        public virtual Guid? ThreadId { get; set; }
        public virtual string CopyTo { get; set; }

        public virtual string Brief { get; set; }

        public virtual int Status { get; set; }

        public virtual int ResultType { get; set; }

        public virtual string AddtionalRecipients { get; set; }
    }
}
