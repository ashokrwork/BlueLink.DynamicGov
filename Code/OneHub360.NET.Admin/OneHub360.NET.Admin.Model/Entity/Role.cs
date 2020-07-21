using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Role : NHEntity
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
        /// There are no comments for Description in the schema.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Users in the schema.
        /// </summary>
        public virtual IList<UserInfo> Users
        {
            get;
            set;
        }
    }

}
