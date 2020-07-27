using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public partial class OutgoingLetterNumberAutoCompelete : DB.NHEntity
    {
        public virtual string OutgoingNumber { get; set; }
        public virtual Guid ThreadId { get; set; }
    }
}
