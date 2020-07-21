using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{
    
    public partial class OrganizationUnitTypes : DB.NHEntity {
        public OrganizationUnitTypes() { }
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        public virtual int? Level { get; set; }
        
    }
}
