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
    /// There are no comments for OneHub.NET.TimeManagement.FiscalYear, OneHub.NET.TimeManagement in the schema.
    /// </summary>
    [Serializable()]
    [DataContract(IsReference = true)]
    public partial class FiscalYear : IEntity {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for FiscalYear constructor in the schema.
        /// </summary>
        public FiscalYear()
        {
            this.IsDeleted = false;
            this.PublicHolidays = new List<PublicHoliday>();
            this.Projects = new List<Project>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        [DataMember(Order=1)]
        public virtual System.Guid Id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        [DataMember(Order=2)]
        public virtual string Name
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for StartDate in the schema.
        /// </summary>
        [DataMember(Order=3)]
        public virtual System.Nullable<System.DateTime> StartDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for EndDate in the schema.
        /// </summary>
        [DataMember(Order=4)]
        public virtual System.Nullable<System.DateTime> EndDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for IsDeleted in the schema.
        /// </summary>
        [DataMember(Order=5)]
        public virtual bool IsDeleted
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CreationDate in the schema.
        /// </summary>
        [DataMember(Order=6)]
        public virtual System.DateTime CreationDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LastUpdateDate in the schema.
        /// </summary>
        [DataMember(Order=7)]
        public virtual System.Nullable<System.DateTime> LastUpdateDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CreatedBy in the schema.
        /// </summary>
        [DataMember(Order=8)]
        public virtual string CreatedBy
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LastUpdatedBy in the schema.
        /// </summary>
        [DataMember(Order=9)]
        public virtual string LastUpdatedBy
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for PublicHolidays in the schema.
        /// </summary>
        [DataMember(Order=10, EmitDefaultValue=false)]
        public virtual IList<PublicHoliday> PublicHolidays
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Projects in the schema.
        /// </summary>
        [DataMember(Order=11, EmitDefaultValue=false)]
        public virtual IList<Project> Projects
        {
            get;
            set;
        }
    }

}
