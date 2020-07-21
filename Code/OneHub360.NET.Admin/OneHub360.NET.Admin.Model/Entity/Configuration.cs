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
    public partial class CustomConfiguration : NHEntity
    {

        /// <summary>
        /// There are no comments for Title in the schema.
        /// </summary>
        public virtual string ConfigurationName
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Responsibilities in the schema.
        /// </summary>
        public virtual string ConfigurationValue
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Description in the schema.
        /// </summary>
        public virtual string ConfigurationGroup
        {
            get;
            set;
        }
    }
}
