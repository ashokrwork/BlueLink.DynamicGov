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
    /// There are no comments for OneHub.NET.TimeManagement.Appointment, OneHub.NET.TimeManagement in the schema.
    /// </summary>
    [Serializable()]
    [DataContract(IsReference = true)]
    public partial class Appointment : PlannedItem {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for Appointment constructor in the schema.
        /// </summary>
        public Appointment()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for Subject in the schema.
        /// </summary>
        [DataMember(Order=1)]
        public virtual string Subject
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Location in the schema.
        /// </summary>
        [DataMember(Order=2)]
        public virtual string Location
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for AppointmentStatus in the schema.
        /// </summary>
        [DataMember(Order=3, EmitDefaultValue=false)]
        public virtual AppointmentStatus AppointmentStatus
        {
            get;
            set;
        }
    }

}
