using System.Collections.Generic;

namespace ClaimDigitalize.Models
{
    public class VehicleInfoVM
    {
        public int RegisterationYear { get; set; }
        public int? SalesFormDetail_Id { get; set; }
        public int? Salesformmapping_Id { get; set; }
        public List<ClaimInfoVM> ClaimList { get; set; }
        public string Chasis_No { get; set; }
        public string Engine_No { get; set; }
        public string ExpiryDate { get; set; }
        public string RegistrationNo { get; set; }
        public string StartDate { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleStatus { get; set; }
    }
}