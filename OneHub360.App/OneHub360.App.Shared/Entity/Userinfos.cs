using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{
    
    public  partial class UserInfos : AdminNHEntity { 
        public  UserInfos() { }
        public virtual JobTitles JobTitles { get; set; }
        public virtual OrganizationUnits OrganizationUnits { get; set; }
        public virtual UserInfos UserInfosVal { get; set; }
        public virtual string LoginName { get; set; }
        [NotNullNotEmpty]
        public virtual string Password { get; set; }
        [NotNullNotEmpty]
        public virtual string PasswordSalt { get; set; }
        [NotNullNotEmpty]
        public virtual string ArabicFullName { get; set; }
        public virtual string LatinFullName { get; set; }
        [NotNullNotEmpty]
        public virtual string Email { get; set; }
        [NotNullNotEmpty]
        public virtual string Mobile { get; set; }
        public virtual byte[] Photo { get; set; }
        public virtual string PersonalMessage { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string About { get; set; }
        public virtual decimal? OfficePhone { get; set; }
        [NotNullNotEmpty]
        public virtual int Status { get; set; }
       
        
    }
}
