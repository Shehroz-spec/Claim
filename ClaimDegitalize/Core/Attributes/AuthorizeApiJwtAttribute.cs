using ClaimDigitalize.Models;
using ClaimDigitalize.Services;
using System;
using System.IdentityModel.Tokens;
using System.Web;
using System.Web.Mvc;

namespace ClaimDigitalize.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeApiJwtAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string auth = string.Empty;
            TokenVM token = new TokenVM();
            string response = string.Empty;
            string jwtAccessTokenString = string.Empty;
            JwtSecurityToken jwtToken = new JwtSecurityToken();

            auth = httpContext.Request.Headers.Get("Authorization");

            if (!string.IsNullOrEmpty(auth))
            {
                string[] splittedAuth = auth.Split(' ');

                if (splittedAuth.Length > 0)
                {
                    token = new TokenVM { AccessToken = splittedAuth[1] };
                }
            }

            // make a remote call and test the validity of this token
            response = HttpExtentions.PostRequest(ConfigurationService.VerifyJwtUrl, token);
            var isAuthorized = !string.IsNullOrEmpty(response) && response != "false";

            return isAuthorized;
        }
    }
}