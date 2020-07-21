using System;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class ExternalGroup : Group
    {
        public ExternalGroup()
        {
            IList<OrganizationUnit> Members = new List<OrganizationUnit>();
        }
        /// <summary>
        /// There are no comments for Members in the schema.
        /// </summary>
        public virtual IList<OrganizationUnit> Members
        {
            get;
            set;
        }
    }

}
