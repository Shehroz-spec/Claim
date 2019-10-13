using System;
using System.Collections.Generic;

namespace ClaimDigitalize.Models
{
    public class ClaimIntimationVM
    {
        public bool ThirdPartytag { get; set; }
        public DateTime Add_On { get; set; }
        public DateTime Edit_On { get; set; }
        public DateTime IncidentDate { get; set; }
        public int Add_By { get; set; }
        public int Area_Id { get; set; }
        public int Branch { get; set; }
        public int Branch_Id { get; set; }
        public int City_Id { get; set; }
        public int ClaimCount { get; set; }
        public int ClaimIntimationAmount { get; set; }
        public int ClaimPaidAmount { get; set; }
        public int ClaimType_Id { get; set; }
        public int CurrentArea_Id { get; set; }
        public int CurrentCity_Id { get; set; }
        public int Edit_By { get; set; }
        public int RelationWithInsured { get; set; }
        public int VehicleId { get; set; }
        public List<ClaimRemarksVM> RemarksList { get; set; }
        public List<IntimationQuestionAnswerVM> IntimationQuestionAnswerList { get; set; }
        public List<IntimationQuestionAnswerVM> IntimationQusAns { get; set; }
        public List<string> Remarks { get; set; }
        public List<WorkshopVM> WorkshopCollection { get; set; }
        public string Add_IP { get; set; }
        public string Circumstance { get; set; }
        public string Circumstances { get; set; }
        public string Circumtances { get; set; }
        public string Claim_Id { get; set; }
        public string ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimType { get; set; }
        public string ClaimTypeName { get; set; }
        public string CurrentArea { get; set; }
        public string CurrentCity { get; set; }
        public string DamageParts { get; set; }
        public string DeductibleAmount { get; set; }
        public string Edit_IP { get; set; }
        public string Email { get; set; }
        public string IncidentArea { get; set; }
        public string IncidentCity { get; set; }
        public string InsuredName { get; set; }
        public string Mobile { get; set; }
        public string MobileNo { get; set; }
        public string NoOfDamagedParts { get; set; }
        public string Outstanding { get; set; }
        public string PhoneNo { get; set; }
        public string Reporter_Id { get; set; }
        public string Salesformdetailid { get; set; }
        public string Salesformmapping { get; set; }
        public string SalesFormNo { get; set; }
        public string ThirdPartyContact { get; set; }
        public string ThirdPartyRemarks { get; set; }
        public string UserId { get; set; }
        public string VehicleAvailablity { get; set; }
        public string Workshop_Id { get; set; }
        public string WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopType { get; set; }
        public string ReserveAmount { get; set; }
        public List<GetAnswer> getAnswer { get; set; } // updated
    }

    public class GetAnswer // updated to get workshop lists
    {
        public string question_Id { get; set; }
        public string answer { get; set; }
        public string answer1 { get; set; }
    }
}