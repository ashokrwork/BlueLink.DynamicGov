using NHibernate.Validator.Constraints;
using OneHub360.DB;
using System.Runtime.Serialization;

namespace OneHub360.CMS.DAL
{

    public class ExternalOrganizations : NHEntity
    {
        
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        public virtual bool? IsG2G { get; set; }
        [NotNullNotEmpty]
        public virtual bool IsActive { get; set; }
        public virtual int G2GSiteID { get; set; }
        [IgnoreDataMember]
        public virtual byte[] Logo { get; set; }

        public virtual string G2GSiteName { get; set; }

        public virtual string PersonTitle { get; set; }

        public virtual string LogoUrl
        {
            get
            {
                return string.Format("{0}cms/externalorganizations/logo/{1}", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, Id);
            }
        }
    }
}

