using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.DB
{
    public class CreationInfo
    {
        public virtual Nullable<Guid> Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual Language Language { get; set; }
        public virtual DateTime LastModified { get; set; }
    }
}
