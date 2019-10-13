using System;

namespace ClaimDigitalize.Models
{
    public class ClaimRemarksVM
    {
        public string RemarksId { get; set; }
        public string Username { get; set; }
        public string ClaimForm_Id { get; set; }
        public string Remarks { get; set; }
        public string Add_By { get; set; }
        public DateTime Add_On { get; set; }
        public string Add_Ip { get; set; }
    }
}