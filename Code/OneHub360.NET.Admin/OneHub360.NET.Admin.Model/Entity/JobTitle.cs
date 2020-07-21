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
    public partial class JobTitle : NHEntity
    {

        /// <summary>
        /// There are no comments for Title in the schema.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Responsibilities in the schema.
        /// </summary>
        public virtual string Responsibilities
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Rank in the schema.
        /// </summary>
        public virtual System.Nullable<int> Rank
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
    }
}
