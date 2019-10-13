using System.Configuration;

namespace ClaimDigitalize.Services
{
    public static class ConfigurationService
    {
        public static string SaveClaimRemarks { get { return ConfigurationManager.AppSettings["SaveClaimRemarks"]; } }
        public static string VerifyJwtUrl { get { return ConfigurationManager.AppSettings["VerifyJwtUrl"]; } }
        public static string JwtAuthUrl { get { return ConfigurationManager.AppSettings["JwtAuthUrl"]; } }
        public static string LoginUrl { get { return ConfigurationManager.AppSettings["LoginUrl"]; } }
        public static string PolicyInfoByParam { get { return ConfigurationManager.AppSettings["PolicyInfoByParam"]; } }
        public static string ClaimIntimate { get { return ConfigurationManager.AppSettings["ClaimIntimate"]; } }
        public static string UpdateClaim { get { return ConfigurationManager.AppSettings["UpdateClaim"]; } }
        public static string ClaimDetailByVehicle { get { return ConfigurationManager.AppSettings["ClaimDetailByVehicle"]; } }
        public static string ClaimType { get { return ConfigurationManager.AppSettings["ClaimType"]; } }
        public static string City { get { return ConfigurationManager.AppSettings["City"]; } }
        public static string Area { get { return ConfigurationManager.AppSettings["Area"]; } }
        public static string Relation { get { return ConfigurationManager.AppSettings["Relation"]; } }
        public static string DamageParts { get { return ConfigurationManager.AppSettings["DamageParts"]; } }
        public static string Workshop { get { return ConfigurationManager.AppSettings["Workshop"]; } }
        public static string TrakkerCompany { get { return ConfigurationManager.AppSettings["TrakkerCompany"]; } }
        public static string ClaimDetail { get { return ConfigurationManager.AppSettings["ClaimDetail"]; } }
        public static string CheckClaimExist { get { return ConfigurationManager.AppSettings["CheckClaimExist"]; } }
        public static string ClaimDocumentList { get { return ConfigurationManager.AppSettings["ClaimDocumentList"]; } }
        public static string ClaimReserve { get { return ConfigurationManager.AppSettings["ClaimReserve"]; } }
        public static string ClaimTypeName { get { return ConfigurationManager.AppSettings["ClaimTypeName"]; } }
        public static string RemarksDetail { get { return ConfigurationManager.AppSettings["RemarksDetail"]; } }
        public static string WorkshopDetail { get { return ConfigurationManager.AppSettings["WorkshopDetail"]; } }
        public static string Branch { get { return ConfigurationManager.AppSettings["Branch"]; } }
    }
}