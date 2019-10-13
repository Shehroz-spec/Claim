using System.Collections.Generic;
using System.Web.Mvc;

namespace ClaimDigitalize.Models
{
    public class PolicyObjectVM
    {
        public int EVAC { get; set; }
        public List<DocumentListVM> DocumentList { get; set; }
        public List<SelectListItem> GetBranch { get; set; }
        public List<SelectListItem> GetCity { get; set; }
        public List<SelectListItem> GetClaimType { get; set; }
        public List<SelectListItem> GetDamageParts { get; set; }
        public List<SelectListItem> GetRelation { get; set; }
        public List<SelectListItem> GetTrakkerCompany { get; set; }
        public string VehicleColor { get; set; }
        public string ChassisNo { get; set; }
        public string Claimcount { get; set; }
        public string ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimTypeName { get; set; }
        public string Contact_Id { get; set; }
        public string DeductibleAmount { get; set; }
        public string Email { get; set; }
        public string EngineNo { get; set; }
        public string ExpiryDate { get; set; }
        public string InsuredName { get; set; }
        public string MobileNo { get; set; }
        public string Outstanding { get; set; }
        public string Package_Id { get; set; }
        public string PhoneNo { get; set; }
        public string PolicyEndDate { get; set; }
        public string PolicyStartDate { get; set; }
        public string RegisterationNo { get; set; }
        public string SalesFormDetail_Id { get; set; }
        public string SalesFormMapping_Id { get; set; }
        public string SalesFormNo { get; set; }
        public string VehicleEndDate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleStartDate { get; set; }
        public string YearOfManufacture { get; set; }
        public ClaimReserve ClaimReserve { get; set; }
    }

    public class ClaimReserve
    { 
        public string ReserveAmount { get; set; }
        public string ReserveAmountT { get; set; }
        public string ReserveRemarks { get; set; }
    }
}