using System;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class InternalGroup : Group
    {
        public InternalGroup()
        {
            IList<UserInfo> Members = new List<UserInfo>();
        }
        /// <summary>
        /// There are no comments for Members in the schema.
        /// </summary>
        public virtual IList<UserInfo> Members
        {
            get;
            set;
        }
    }

}
