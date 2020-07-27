using NHibernate.Validator.Constraints;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    [Serializable]
    public class CorrespondenceDocuments : NHEntity
    {
        
        [NotNullNotEmpty]
        public virtual System.Guid FK_Correspondence { get; set; }
        [NotNullNotEmpty]
        public virtual System.Guid FK_Document { get; set; }
        

        public virtual bool? IsMainDocument { get; set; }
        
    }
}
