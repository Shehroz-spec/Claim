using ClaimDigitalize.Models;
using ClaimDigitalize.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;
using System.Web.UI;
using Vereyon.Web;

namespace ClaimDigitalize.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            loginVM.GrantType = "password";

            try
            {
                string AccessToken = HttpExtentions.Post(ConfigurationService.JwtAuthUrl, loginVM);
                object token = JsonConvert.DeserializeObject(AccessToken);
                string accessToken = JObject.Parse(token.ToString())["access_token"].ToString();

                if (!string.IsNullOrEmpty(AccessToken))
                {
                    Session["JwtAccessToken"] = AccessToken;
                    return RedirectToAction("Index", "Claim", accessToken);
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
                FlashMessage.Danger(ValidationMessages.InvalidUser);
                return RedirectToAction("Login", "Account");
            }

            FlashMessage.Danger(ValidationMessages.InvalidUser);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult CheckSessionTimeout()
        {
            var UserId = System.Web.HttpContext.Current.Session["UserId"];

            if (string.IsNullOrEmpty(UserId.ToString()))
            {
                return RedirectToAction("Logoff", "Account");
            }

            UserVM user = new UserVM
            {
                UserId = Convert.ToInt32(UserId)
            };

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Account");
        }
    }
}