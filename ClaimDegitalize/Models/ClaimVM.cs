using System.Collections.Generic;

namespace ClaimDigitalize.Models
{
    public class ClaimVM
    {
        public List<ClaimRemarksVM> RemarksList { get; set; }
        public List<string> Remarks { get; set; }
        public List<WorkshopVM> WorkshopCollection { get; set; }
        public string Branch { get; set; }
        public string ChassisNo { get; set; }
        public string Circumstances { get; set; }
        public string Claimcount { get; set; }
        public string ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimType { get; set; }
        public string ClaimTypeName { get; set; }
        public string ComplaintNo { get; set; }
        public string Contact_Id { get; set; }
        public string CurrentArea { get; set; }
        public string CurrentCity { get; set; }
        public string DamageDetail1 { get; set; }
        public string DamageDetail2 { get; set; }
        public string DamageDetailRemarks { get; set; }
        public string Damageparts { get; set; }
        public string DeductibleAmount { get; set; }
        public string Email { get; set; }
        public string EngineNo { get; set; }
        public string EVAC { get; set; }
        public string ExpiryDate { get; set; }
        public string HeavyDamagePart { get; set; }
        public string HeavyDamagePartRemarks { get; set; }
        public string IncidentArea { get; set; }
        public string IncidentCity { get; set; }
        public string IncidentDate { get; set; }
        public string InsuredName { get; set; }
        public string IntimateService { get; set; }
        public string IsRequireRentaCar { get; set; }
        public string IsRequireTowTruck { get; set; }
        public string IsThirdPartyAccident { get; set; }
        public string MobileNo { get; set; }
        public string NameInsuranceCompany { get; set; }
        public string ObjectDetail { get; set; }
        public string ObjectLocation { get; set; }
        public string ObjectType { get; set; }
        public string Outstanding { get; set; }
        public string Package_Id { get; set; }
        public string PhoneNo { get; set; }
        public string PolicyEndDate { get; set; }
        public string PolicyPeriod { get; set; }
        public string PolicyReport { get; set; }
        public string PolicyStartDate { get; set; }
        public string RegisterationNo { get; set; }
        public string Relation { get; set; }
        public string RelationWithInsured { get; set; }
        public string Salesformdetailid { get; set; }
        public string SalesFormMapping_Id { get; set; }
        public string SalesFormNo { get; set; }
        public string TrackerCompanyName { get; set; }
        public string TrakkerInstall { get; set; }
        public string UserId { get; set; }
        public string VehicleAvailablity { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleDetailDD { get; set; }
        public string VehicleEndDate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleStartDate { get; set; }
        public string Workshop { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopType { get; set; }
        public string YearOfManufacture { get; set; }
        public string ReserveAmount { get; set; }
    }
}