using System;
using System.Text;
using System.Collections.Generic;


namespace OneHub360.CMS.DAL {
    
    public  class Report : DB.NHEntity{
        
        
        public virtual System.Guid ReportType { get; set; }
        public virtual System.Guid ReportOfficier { get; set; }
        public virtual string CaseNumber { get; set; }
        public virtual string ReportSubject { get; set; }
        public virtual DateTime IncidentDate { get; set; }
        public virtual DateTime ReportDate { get; set; }
        public virtual DateTime RecordingDate { get; set; }
        public virtual System.Guid ReportStatus { get; set; }
        public virtual System.Guid Investigator { get; set; }
        public virtual string ReportDescription { get; set; }
        public virtual string CaseYear { get; set; }
        public virtual System.Guid ReportGovernance { get; set; }
        public virtual string ReportArea { get; set; }
       
    }
}
