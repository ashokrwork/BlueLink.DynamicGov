using NHibernate.Validator.Constraints;
using System;

namespace OneHub360.App.Shared
{
    public partial class UserActionView : DB.NHEntity
    {
        [NotNullNotEmpty]
        public virtual System.Guid Id { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime CreationDate { get; set; }
        public virtual bool? IsDeleted { get; set; }
        public virtual int? Language { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid EntityId { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastModified { get; set; }
        
        [NotNullNotEmpty]
        public virtual System.Guid FK_Feed { get; set; }
        [NotNullNotEmpty]
        public virtual string CreatedBy { get; set; }
        public virtual System.Nullable<System.Guid> FK_Parent { get; set; }
        [NotNullNotEmpty]
        public virtual bool IsConfidential { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid Actor { get; set; }
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
        public virtual System.Guid ThreadId { get; set; }
        [NotNullNotEmpty]
        public virtual string Destination { get; set; }
        public virtual string Notes { get; set; }
        [NotNullNotEmpty]
        public virtual string Message { get; set; }
        [NotNullNotEmpty]
        public virtual string ActionCssClass { get; set; }
        [NotNullNotEmpty]
        public virtual string TemplateUrl { get; set; }
        public virtual string Subject { get; set; }
    }
}
