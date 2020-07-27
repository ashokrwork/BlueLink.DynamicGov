using NHibernate.Validator.Constraints;
using OneHub360.DB;
using System;


namespace OneHub360.CMS.G2G.DAL
{

    public partial class G2GAction : INHEntity
    {
        public virtual  int G2GActionID { get; set; }
        [NotNullNotEmpty]
        public virtual  bool G2GActionType { get; set; }
        [NotNullNotEmpty]
        public virtual  string SSite { get; set; }
        [NotNullNotEmpty]
        public virtual  string RSite { get; set; }
        [NotNullNotEmpty]
        public virtual  string SADName { get; set; }
        [NotNullNotEmpty]
        public virtual  DateTime SDate { get; set; }
        [NotNullNotEmpty]
        public virtual  string SDocVsID { get; set; }
        [NotNullNotEmpty]
        public virtual  string SAutoReferenceNo { get; set; }
        public virtual  string G2GSubject { get; set; }
        [NotNullNotEmpty]
        public virtual  int G2GStatus { get; set; }
        public virtual  string RADName { get; set; }
        public virtual  DateTime? RDate { get; set; }
        public virtual  string RDocVsID { get; set; }
        public virtual  string RAutoReferenceNo { get; set; }
        public virtual  string AADName { get; set; }
        [NotNullNotEmpty]
        public virtual  bool IsArchived { get; set; }
        public virtual  DateTime? ADate { get; set; }
        public virtual  int? SG2GActionID { get; set; }
        public virtual  string G2GSRemarks { get; set; }
        public virtual  string G2GRRemarks { get; set; }
        public virtual  string SArchRefNo { get; set; }
        public virtual  int? SBrSiteID { get; set; }
        public virtual  int? RBrSiteID { get; set; }

        public Guid Id
        {
            get; set;
        }

        public DateTime CreationDate
        {
            get; set;
        }

        public DateTime LastModified
        {
            get; set;
        }

        public bool IsDeleted
        {
            get; set;
        }

        public string CreatedBy
        {
            get; set;
        }

        public Language Language
        {
            get; set;
        }
    }
}
