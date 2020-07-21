using System;
using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class EntityType : NHEntity
    {
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
    }

}
