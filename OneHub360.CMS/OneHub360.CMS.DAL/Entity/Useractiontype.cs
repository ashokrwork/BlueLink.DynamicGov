using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using OneHub360.DB;

namespace OneHub360.CMS.DAL
{

    public partial class UserActionType : OneHub360.DB.NHEntity
    {
        public virtual System.Guid Id { get; set; }
        [NotNullNotEmpty]
        public virtual string CreatedBy { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime CreationDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastModified { get; set; }
        public virtual bool? IsDeleted { get; set; }
        public virtual Language Language { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid EntityId { get; set; }
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        [NotNullNotEmpty]
        public virtual string Message { get; set; }
    }
}
