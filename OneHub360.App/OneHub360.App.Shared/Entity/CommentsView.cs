using NHibernate.Validator.Constraints;
using OneHub360.DB;
using System;

namespace OneHub360.App.Shared
{
    public class CommentsView : NHEntity
    {
        [NotNullNotEmpty]
        public virtual Guid Id { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime LastModified { get; set; }
        public virtual bool? IsDeleted { get; set; }
        public virtual int? Language { get; set; }

        [NotNullNotEmpty]
        public virtual Guid FK_Feed { get; set; }
        public virtual Guid? FK_Parent { get; set; }
        [NotNullNotEmpty]
        public virtual Guid FK_User { get; set; }
        public virtual string Body { get; set; }
        [NotNullNotEmpty]
        public virtual bool Private { get; set; }
        

        public virtual Guid? ThreadId { get; set; }
        public virtual string Picture
        {
            get
            {
                return string.Format("{0}api/user/picture/{1}", AppConfigLoader.Generator.configData.AppApiBaseUrl, FK_User);
            }
        }
    }
}
