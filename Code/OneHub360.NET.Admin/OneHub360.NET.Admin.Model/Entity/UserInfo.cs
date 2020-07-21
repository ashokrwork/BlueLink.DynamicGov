using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class UserInfo : NHEntity
    {
        public UserInfo()
        {
            //this.Groups = new List<Group>();
            //this.Signatures = new List<Signature>();
            //this.Roles = new List<Role>();
            Status = UserStatus.Registered;
           
        }

        /// <summary>
        /// There are no comments for LoginName in the schema.
        /// </summary>
        public virtual string LoginName
        {
            get;
            set;
        }
        public virtual string ADUsername
        {
            get;
            set;
        }
        /// <summary>
        /// There are no comments for Password in the schema.
        /// </summary>
        public virtual string Password
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for PasswordSalt in the schema.
        /// </summary>
        public virtual string PasswordSalt
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for ArabicFullName in the schema.
        /// </summary>
        public virtual string ArabicFullName
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for LatinFullName in the schema.
        /// </summary>
        public virtual string LatinFullName
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Email in the schema.
        /// </summary>
        public virtual string Email
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Mobile in the schema.
        /// </summary>
        public virtual string Mobile
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Photo in the schema.
        /// </summary>
        public virtual byte[] Photo
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for PersonalMessage in the schema.
        /// </summary>
        public virtual string PersonalMessage
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for BirthDate in the schema.
        /// </summary>
        public virtual Nullable<DateTime> BirthDate
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
        /// There are no comments for OfficePhone in the schema.
        /// </summary>
        public virtual Nullable<decimal> OfficePhone
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual UserStatus Status
        {
            get;
            set;
        }
        /// <summary>
        /// There are no comments for OrganizationUnit in the schema.
        /// </summary>
        [IgnoreDataMember]
        public virtual OrganizationUnit OrganizationUnit
        {
            get;
            set;
        }
        public virtual string OrganizationUnitID
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for JobTitle in the schema.
        /// </summary>
        public virtual JobTitle JobTitle
        {
            get;
            set;
        }
        public virtual string JobTitleID
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for ReportingTo in the schema.
        /// </summary>
        public virtual UserInfo ReportingTo
        {
            get;
            set;
        }
        public virtual string ReportingToID
        {
            get;
            set;
        }
        public virtual  List<string> GroupsIds
        {
            get;
            set;
        }
        public virtual List<string> RolesIds
        {
            get;
            set;
        }
        /// <summary>
        /// There are no comments for Groups in the schema.
        /// </summary>
        //[IgnoreDataMember]
        //public virtual IList<Group> Groups
        //{
        //    get;
        //    set;
        //}

        //public virtual List<Group> GroupsList
        //{
        //    get
        //    {
        //        return this.Groups.ToList<Group>();
        //    }
        //    set
        //    {
        //        Groups = value;
        //    }
        //}
        ///// <summary>
        ///// There are no comments for Signatures in the schema.
        ///// </summary>
        //public virtual IList<Signature> Signatures
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// There are no comments for Roles in the schema.
        ///// </summary>
        //public virtual IList<Role> Roles
        //{
        //    get;
        //    set;
        //}
    }

}
