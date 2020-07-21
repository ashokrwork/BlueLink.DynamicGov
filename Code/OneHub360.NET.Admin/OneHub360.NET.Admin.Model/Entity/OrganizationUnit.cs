using OneHub360.NET.Core.Model;
using System;
using System.Configuration;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class OrganizationUnit : NHEntity
    {
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }
        public virtual string Prifix
        {
            get;
            set;
        }
        public virtual int LastGeneratedNumber
        {
            get;
            set;
        }

        public virtual string PersonTitle
        {
            get;
            set;
        }
        public virtual byte[] Logo
        {
            get;
            set;
        }
        /// <summary>
        /// There are no comments for About in the schema.
        /// </summary>
        public virtual string About
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Location in the schema.
        /// </summary>
        public virtual string Location
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for OrganizationUnitParent in the schema.
        /// </summary>
        public virtual OrganizationUnit OrganizationUnitParent
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Manager in the schema.
        /// </summary>
        public virtual Guid ManagerId
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Organization in the schema.
        /// </summary>
        public virtual Organization Organization
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for OrganizationUnitType in the schema.
        /// </summary>
        public virtual OrganizationUnitType OrganizationUnitType
        {
            get;
            set;
        }
      
        public virtual string OrganizationUnitParentID
        {
            get;
            set;
        }


        public virtual string OrganizationID
        {
            get;
            set;
        }

       
        public virtual string OrganizationUnitTypeID
        {
            get;
            set;
        }
        public virtual string LogoUrl
        {
            get
            {
                return string.Format("{0}api/organizationunit/picture?id={1}", ConfigurationManager.AppSettings["Apiurl"], Id);
            }
        }
        
    }

}
