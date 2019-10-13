using ClaimDigitalize.Filter;
using ClaimDigitalize.Models;
using ClaimDigitalize.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System;

namespace ClaimDigitalize.Controllers
{
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class ClaimController : Controller
    {
        private string Authorization => HttpContext.Request.Headers.Get("Authorization");
        private readonly string apiUrl = string.Empty;
        private readonly ClaimService _claimService = new ClaimService();

        //[AuthorizeViewJwt(Roles = "administrator")]
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }

        [HandleError]
        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        public ActionResult Edit()
        {
            return View("Edit", Session["PolicyObject"]);
        }

        [HandleError]
        public ActionResult ViewClaim()
        {
            return View("ViewClaim", Session["PolicyObject"]);
        }

        [AuthorizeApiJwt]
        [HttpGet]
        public JsonResult GetBranch()
        {
            List<SelectListItem> response = _claimService.GetBranch();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpGet]
        public JsonResult GetAreaByCity(int? cityId)
        {
            return Json(_claimService.GetAreaByCity(cityId), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpGet]
        public JsonResult GetWorkshop(string workshopType)
        {
            List<SelectListItem> workshopSelectListItems = _claimService.GetWorkshopType(workshopType);
            return Json(workshopSelectListItems, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public ActionResult GetClaimTypeContent(string claimTypeName)
        {
            claimTypeName = claimTypeName.Replace("/", " ");
            string result = _claimService.GetDocumentList(claimTypeName);
            string claimReserveResult = _claimService.GetClaimReserve(claimTypeName);
            ClaimReserve claimReserve = new ClaimReserve();

            if (!string.IsNullOrEmpty(claimReserveResult))
            {
                claimReserveResult = claimReserveResult.Replace("[", "").Replace("]", "");
                claimReserve = JsonConvert.DeserializeObject<ClaimReserve>(claimReserveResult);
            }

            if (result == null)
            {
                return PartialView("_Accident", Session["PolicyObject"]);
            }

            List<DocumentListVM> documentList = JsonConvert.DeserializeObject<List<DocumentListVM>>(result);
            PolicyObjectVM policyObject = (PolicyObjectVM)Session["PolicyObject"];
            policyObject.DocumentList = documentList;
            policyObject.ClaimReserve = claimReserve;
            Session["PolicyObject"] = policyObject;

            Dictionary<string, string> viewsDictionary = new Dictionary<string, string>
            {
                { "Accident", "_Accident" },
                { "Accessory Theft", "_AccessoriesTheft" },
                { "Rain Water Damage", "_RainWaterDamage" },
                { "Third Party", "_ThirdPartyCoverage" },
                { "Theft", "_TheftSnatch" },
                { "Snatch", "_TheftSnatch" }
            };

            return PartialView(viewsDictionary[claimTypeName], policyObject);
        }

        private ActionResult GetClaimPartialByModuleName(ClaimIntimationVM claimIntimationViewModel)
        {
            switch (claimIntimationViewModel.ClaimTypeName)
            {
                case "Third Party":
                case "Third Party Coverage":
                    return Json(_claimService.GetThirdPartyCoverage(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                case "Accident":
                    return Json(_claimService.GetAccidentRainCoverage(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                case "Rain Water Damage":
                    return Json(_claimService.GetAccidentRainCoverage(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                case "Theft":
                    return Json(_claimService.GetTheftSnatch(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                case "Snatch":
                    return Json(_claimService.GetTheftSnatch(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                case "Accessory Theft":
                    return Json(_claimService.GetAccessoryTheft(claimIntimationViewModel), JsonRequestBehavior.AllowGet);
                default:
                    return Json(claimIntimationViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeApiJwt]
        [HttpGet]
        public ActionResult GetClaimDetail(int? claimId)    // updated to get workshop list 
        {
            string result = HttpExtentions.GetRequest($"{ConfigurationService.ClaimDetail}{claimId}", Authorization);
            ClaimIntimationVM claimIntimationViewModel = JsonConvert.DeserializeObject<ClaimIntimationVM>(result);
            string remarksResult = HttpExtentions.GetRequest($"{ConfigurationService.RemarksDetail}{claimId}", Authorization);
            List<ClaimRemarksVM> remarksList = JsonConvert.DeserializeObject<List<ClaimRemarksVM>>(remarksResult);
            claimIntimationViewModel.WorkshopCollection = _claimService.LoadWorkshopDetail(Authorization, claimId, claimIntimationViewModel);
            claimIntimationViewModel.RemarksList = remarksList;
            return GetClaimPartialByModuleName(claimIntimationViewModel);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public ActionResult GetSearchResult(SearchVM searchDetailViewModel)
        {
            List<SearchDetailVM> searchDetailsVmList = HttpExtentions.PostRequest<List<SearchDetailVM>>(WebConfigurationManager.AppSettings["PolicyInfoByParam"], searchDetailViewModel, Authorization);
            ResponseVM<List<PolicyInfoVM>> searchResponse = _claimService.GetSearchDetail(searchDetailsVmList, searchDetailViewModel, Authorization);

            return PartialView("_Search", searchResponse.Data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClaim(ClaimVM claimVM)
        {
            PolicyObjectVM claimObject = _claimService.GetPolicyObject(claimVM);
            Session["PolicyObject"] = claimObject;
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult EditDetail(ClaimVM claimVM)
        {
            Session.Remove("PolicyObject");
            PolicyObjectVM claimObject = _claimService.GetPolicyObject(claimVM);
            Session["PolicyObject"] = claimObject;
            return Json(claimObject, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ViewDetail(ClaimVM claimVM)
        {
            Session.Remove("PolicyObject");
            PolicyObjectVM claimObject = _claimService.GetPolicyObject(claimVM);
            Session["PolicyObject"] = claimObject;
            return Json(claimObject, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public JsonResult SaveAccident(ClaimVM claimVM)
        {
            List<ClaimGeneratedVM> generatedClaimList = _claimService.SaveClaim(claimVM, Authorization, "Accident");
            return Json(generatedClaimList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public JsonResult SaveTheftSnatch(ClaimVM claimVM)
        {
            List<ClaimGeneratedVM> generatedClaimList = _claimService.SaveClaim(claimVM, Authorization, "TheftSnatch");
            return Json(generatedClaimList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public JsonResult SaveAccessoriesTheft(ClaimVM claimVM)
        {
            List<ClaimGeneratedVM> generatedClaimList = _claimService.SaveClaim(claimVM, Authorization, "AccessoryTheft");
            return Json(generatedClaimList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeApiJwt]
        [HttpPost]
        public JsonResult SaveThirdPartyCoverage(ClaimVM claimVM)
        {
            List<ClaimGeneratedVM> generatedClaimList = _claimService.SaveClaim(claimVM, Authorization, "ThirdParty");
            return Json(generatedClaimList, JsonRequestBehavior.AllowGet);
        }
    }
}