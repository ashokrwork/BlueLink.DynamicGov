using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.CMS.DAL {
    
    public class Template : DB.NHEntity {
        public Template() { }
        
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        public virtual byte[] File { get; set; }
        [NotNullNotEmpty]
        public virtual int Status { get; set; }
        public virtual string Filename { get; set; }
    }
}
