/*
 * This code belongs to Hewlett Packard Enterprise
 * Copyright © 2016 HPE -  All rights are reserved worldwide
 */

using System;
using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Organization : NHEntity
    {
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
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
        /// There are no comments for URL in the schema.
        /// </summary>
        public virtual string URL
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
        /// There are no comments for Fax in the schema.
        /// </summary>
        public virtual string Fax
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for IsLocal in the schema.
        /// </summary>
        public virtual bool IsLocal
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Address in the schema.
        /// </summary>
        public virtual string Address
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for OfficeNumber in the schema.
        /// </summary>
        public virtual string OfficeNumber
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for OrganizationType in the schema.
        /// </summary>
        public virtual OrganizationType OrganizationType
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Email in the schema.
        /// </summary>
        public virtual string OrganizationTypeID
        {
            get
           ;
            set ;
        }
    }
}
