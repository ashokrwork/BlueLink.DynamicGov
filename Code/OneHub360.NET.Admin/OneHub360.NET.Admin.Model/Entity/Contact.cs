using System;
using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Contact : NHEntity
    {
        /// <summary>
        /// There are no comments for ListingOrder in the schema.
        /// </summary>
        public virtual Nullable<int> ListingOrder
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for IsFavourite in the schema.
        /// </summary>
        public virtual Nullable<bool> IsFavourite
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Owner in the schema.
        /// </summary>
        public virtual UserInfo Owner
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for UserInfo in the schema.
        /// </summary>
        public virtual UserInfo UserInfo
        {
            get;
            set;
        }
    }
}
