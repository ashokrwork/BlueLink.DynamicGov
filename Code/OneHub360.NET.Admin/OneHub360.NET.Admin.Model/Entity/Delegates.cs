using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Delegates : NHEntity
    {
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual DateTime? Fromdate
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Description in the schema.
        /// </summary>
        public virtual DateTime? Todate
        {
            get;
            set;
        }
        public virtual System.Nullable<System.Guid> Delegateid { get; set; }
        public virtual System.Nullable<System.Guid> Delegatorid { get; set; }
        /// <summary>
        /// There are no comments for Users in the schema.
        /// </summary>
  
        public virtual UserInfo DelegatorUser
        {
            get;
            set;
        }
        public virtual UserInfo DelegeteUser
        {
            get;
            set;
        }
    }

}
