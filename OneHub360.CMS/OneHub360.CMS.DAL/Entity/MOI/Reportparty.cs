using System;
using System.Text;
using System.Collections.Generic;


namespace OneHub360.CMS.DAL {
    
    public class ReportParty : DB.NHEntity {
        
        public virtual System.Guid ReportId { get; set; }
        public virtual string PartyArabicName { get; set; }
        public virtual System.Guid PartyIdentificationType { get; set; }
        public virtual string PartyIdentificationNumber { get; set; }
        public virtual string PartyEnglishName { get; set; }
        public virtual System.Guid PartyNationality { get; set; }
        public virtual System.Guid PartyGender { get; set; }
        public virtual System.Guid PartyReligion { get; set; }
        public virtual DateTime PartyBirthDate { get; set; }
        public virtual string PartyAddress { get; set; }
        public virtual string PartyMobilePhone { get; set; }
        public virtual System.Guid PartySponsorType { get; set; }
        public virtual string PartySponsorName { get; set; }
        public virtual string PartySponsorMobilePhone { get; set; }
        public virtual string PartyJobTitle { get; set; }
        public virtual string PartyMailBox { get; set; }
        public virtual System.Nullable<System.Guid> PartyDrivingLicenseType { get; set; }
        public virtual string PartyDrivingLicenseNumber { get; set; }
        public virtual DateTime? PartyDrivingLicenseStartDate { get; set; }
        public virtual DateTime? PartyDrivingLicenseEndDate { get; set; }
        public virtual System.Nullable<System.Guid> PartyCarType { get; set; }
        public virtual string PartyCarPlateNumber { get; set; }
        public virtual string PartyCarManufactureYear { get; set; }
        public virtual string PartyCarOwnerName { get; set; }
        public virtual string PartyCarOwnerAddress { get; set; }
        public virtual string PartyCarInsuranceNumber { get; set; }
        public virtual System.Nullable<System.Guid> PartyCarInsuranceType { get; set; }
        public virtual string PartyCarInsuranceCompanyName { get; set; }
        public virtual DateTime? PartyCarInsuranceStartDate { get; set; }
        public virtual DateTime? PartyCarInsuranceEndDate { get; set; }
        
    }
}
