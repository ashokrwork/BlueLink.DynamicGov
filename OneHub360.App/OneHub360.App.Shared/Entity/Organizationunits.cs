using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{
    
    public  partial class OrganizationUnits : DB.NHEntity {
        public  OrganizationUnits() { }
       
        public virtual OrganizationUnits OrganizationUnitsVal { get; set; }
        public virtual UserInfos UserInfos { get; set; }
        public virtual OrganizationUnitTypes OrganizationUnitTypes { get; set; }
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        public virtual string About { get; set; }
        public virtual string Location { get; set; }
    }
}
