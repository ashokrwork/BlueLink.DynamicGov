using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business
{
    public partial class IncomingMemoAction
    {
        public virtual string Subject { get; set; }
        public virtual Guid FK_From { get; set; }
        public virtual Guid FK_IncomingMemoId { get; set; }
        public virtual Guid FK_To { get; set; }
        public virtual string Brief { get; set; }
        public virtual string Pin { get; set; }
        public virtual Guid[] SelectedAttachements { get; set; }
        public virtual bool AttachAll { get; set; }
        public virtual bool Send { get; set; }

        public virtual Guid ThreadId { get; set; }

        public virtual bool IsReply { get; set; }
    }
}
