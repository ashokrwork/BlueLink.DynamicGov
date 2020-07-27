using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public partial class KeywordSearchResult
    {
        public virtual Guid Id { get; set; }
        public virtual string Subject { get; set; }
        public virtual string From { get; set;}
        public virtual string To { get; set; }
        public virtual int ResultType { get; set; }
        public virtual string Brief { get; set; }
    }
}
