using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Validator.Constraints;

namespace OneHub360.App.Shared
{
    public class SharingView : DB.NHEntity
    {
        public SharingView() { }
        [NotNullNotEmpty]
        public virtual Guid Id { get; set; }
        public virtual string SharedWith { get; set; }
    }
}
