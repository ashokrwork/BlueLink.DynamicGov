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
    /// There are no comments for OneHub.NET.TimeManagement.Meeting, OneHub.NET.TimeManagement in the schema.
    /// </summary>
    [Serializable()]
    [DataContract(IsReference = true)]
    public partial class Meeting : PlannedItem {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for Meeting constructor in the schema.
        /// </summary>
        public Meeting()
        {
            this.MeetingInvitations = new List<MeetingInvitation>();
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
        /// There are no comments for MeetingInvitations in the schema.
        /// </summary>
        [DataMember(Order=3, EmitDefaultValue=false)]
        public virtual IList<MeetingInvitation> MeetingInvitations
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for MeetingStatus in the schema.
        /// </summary>
        [DataMember(Order=4, EmitDefaultValue=false)]
        public virtual MeetingStatus MeetingStatus
        {
            get;
            set;
        }
    }

}