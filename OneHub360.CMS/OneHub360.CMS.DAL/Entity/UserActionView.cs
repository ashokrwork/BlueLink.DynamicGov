using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using OneHub360.DB;

namespace OneHub360.CMS.DAL
{

     public partial class UserActionView : NHEntity
    {
        [NotNullNotEmpty]
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
        public virtual string Actor { get; set; }
        [NotNullNotEmpty]
        public virtual string BrowserType { get; set; }
        [NotNullNotEmpty]
        public virtual string MachineIP { get; set; }
        [NotNullNotEmpty]
        public virtual string MachineName { get; set; }
        [NotNullNotEmpty]
        public virtual string ServerName { get; set; }
        [NotNullNotEmpty]
        public virtual string RequestUrl { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_Correspondence { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_UserActionType { get; set; }
        public virtual Guid? ThreadId { get; set; }
        [NotNullNotEmpty]
        public virtual string Destination { get; set; }
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        [NotNullNotEmpty]
        public virtual string Message { get; set; }
        public virtual string ActionCssClass { get; set; }
    }
}
