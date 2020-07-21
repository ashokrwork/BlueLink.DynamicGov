using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{

     public partial class Comment : DB.NHEntity
    {
        
        
        
        
        public virtual Guid FK_Feed { get; set; }

        public virtual Guid FK_Parent { get; set;}
        [NotNullNotEmpty]
        public virtual System.Guid FK_User { get; set; }
        public virtual string Body { get; set; }
       
        [NotNullNotEmpty]
        public virtual bool Private { get; set; }

        public virtual Guid? ThreadId { get; set; }
    }
}
