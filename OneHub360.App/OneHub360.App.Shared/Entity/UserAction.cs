using NHibernate.Validator.Constraints;
using System;

namespace OneHub360.App.Shared
{
    public partial class UserAction : DB.NHEntity
    {
        public virtual Guid Id { get; set; }
        public virtual Guid FK_Feed { get; set; }
        public virtual Guid FK_UserActionType { get; set; }
        [NotNullNotEmpty]
        public virtual string CreatedBy { get; set; }
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
        public virtual Guid? ThreadId { get; set; }
        [NotNullNotEmpty]
        public virtual string Destination { get; set; }
        public virtual string Notes { get; set; }

        public virtual string Subject { get; set; }
    }
}
