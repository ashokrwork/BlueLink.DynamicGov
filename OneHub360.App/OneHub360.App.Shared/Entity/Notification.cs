using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{

    public  partial class Notification : DB.NHEntity
    {
        
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        [NotNullNotEmpty]
        public virtual string Description { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_FeedId { get; set; }
        [NotNullNotEmpty]
        public virtual bool IsRead { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_User { get; set; }

        [NotNullNotEmpty]
        public virtual System.Guid FK_From { get; set; }

        [NotNullNotEmpty]
        public virtual string TemplateUrl { get; set; }
        
    }
}
