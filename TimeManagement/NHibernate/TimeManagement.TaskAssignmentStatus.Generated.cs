﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 16/10/2016 07:46:00 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OneHub.NET.TimeManagement
{

    /// <summary>
    /// There are no comments for OneHub.NET.TimeManagement.TaskAssignmentStatus, OneHub.NET.TimeManagement in the schema.
    /// </summary>
    [Serializable()]
    [DataContract(IsReference = true)]
    public partial class TaskAssignmentStatus : Status {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for TaskAssignmentStatus constructor in the schema.
        /// </summary>
        public TaskAssignmentStatus()
        {
            OnCreated();
        }
    }

}
