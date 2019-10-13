using System.Collections.Generic;

namespace ClaimDigitalize.Models
{
    public class PolicyInfoVM
    {
        public string InsuredName { get; set; }
        public string CustomerName { get; set; }
        public string SalesFormNo { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }
        public string PolicyStatus { get; set; }
        public string Product { get; set; }
        public int? Package_Id { get; set; }
        public List<VehicleInfoVM> VehicleList { get; set; }
        public int? Salesformdetailid { get; set; }
        public int? SalesFormMapping_Id { get; set; }
        public string MobileNo { get; set; }
        public string VAF { get; internal set; }
        public int? Contact_Id { get; set; }
        public string Email { get; set; }
        public int EVAC { get; set; }
        public string DeductibleAmount { get; set; }
        public string Outstanding { get; set; }
        public string Claimcount { get; set; }
        public string EngineNo { get; set; }
        public string ChasisNo { get; set; }
        public string RegisterationNo { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string YearOfManufacture { get; set; }
        public string PolicyStartDate { get; set; }
        public string PolicyEndDate { get; set; }
        public string VehicleStartDate { get; set; }
        public string VehicleEndDate { get; set; }
    }
}