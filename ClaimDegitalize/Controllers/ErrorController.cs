using System.IdentityModel.Tokens;
using System.Linq;
using System.Web.Mvc;

namespace ClaimDigitalize.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.UserId = UserId;

            return View();
        }

        public string UserId
        {
            get
            {
                string auth = this.HttpContext.Request.Headers.Get("Authorization");
                string token = auth.Split(' ')[1];
                var rt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                string userId = rt.Claims.First(claim => claim.Type == "UserId").Value;

                return userId;
            }
        }
    }
}