using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using OneHub360.DB;
using System.Runtime.Serialization;

namespace OneHub360.App.Shared
{
    
    public  partial class UserInfoAutoComplete : AdminNHEntity
    {
        public UserInfoAutoComplete() { }
        
        public override Guid Id { get; set; }
        [IgnoreDataMember()]
        public override bool IsDeleted { get; set; }
        
        [IgnoreDataMember()]
        public override DateTime LastModified { get; set; }

        public virtual string FullName { get; set; }
        public virtual string PersonalMessage { get; set; }
        
        public virtual string About { get; set; }
        
        public virtual string Organization { get; set; }


        public virtual string DisplayName {
            get
            {
                return string.Format("{0} {1}",About,Organization);
            }
        }

        public virtual string Picture
        {
            get
            {
                return string.Format("{0}api/user/picture/{1}", AppConfigLoader.Generator.configData.AppApiBaseUrl, Id);
            }
        }

        public virtual string Mobile { get; set; }
    }
}
