using NHibernate.Validator.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Shared
{
    public partial class UserActionType : DB.NHEntity
    {
        public UserActionType() { }
        public virtual Guid Id { get; set; }
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        [NotNullNotEmpty]
        public virtual string Message { get; set; }
        [NotNullNotEmpty]
        public virtual string ActionCssClass { get; set; }
        [NotNullNotEmpty]
        public virtual string TemplateUrl { get; set; }
    }
}
