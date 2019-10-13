using ClaimDigitalize.Models;
using ClaimDigitalize.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClaimDigitalize.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeViewJwtAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string auth = string.Empty;
            string response = string.Empty;
            string jwtAccessTokenString = string.Empty;
            JwtSecurityToken jwtToken = new JwtSecurityToken();
            List<string> existingRoles;
            List<bool> isAllowedList = new List<bool>();
            bool isAllowed = true;
            bool isAuthorized = false;
            TokenVM token = new TokenVM();

            jwtAccessTokenString = HttpContext.Current.Session["JwtAccessToken"].ToString();
            auth = JObject.Parse(jwtAccessTokenString)["access_token"].ToString();

            if (auth.Length > 0)
            {
                token = new TokenVM { AccessToken = auth };
                // make a remote call and test the validity of this token
                response = HttpExtentions.PostRequest(ConfigurationService.VerifyJwtUrl, token);
                isAuthorized = !string.IsNullOrEmpty(response) && response != "false";

                jwtToken = new JwtSecurityTokenHandler().ReadToken(auth) as JwtSecurityToken;
                existingRoles = Roles.Split(',').ToList();

                foreach (KeyValuePair<string, object> item in jwtToken.Payload)
                {
                    if (item.Key.ToLower().Contains("role"))
                    {
                        IEnumerable<object> roleValue = item.Value as IEnumerable<object>;
                        roleValue.ToList();

                        foreach (object rol in roleValue)
                        {
                            isAllowedList.Add(existingRoles.TrueForAll(x => x.Contains(rol.ToString())));
                        }
                    }
                }
            }

            isAllowed = isAllowedList.Any(x => x);
            return isAuthorized && isAllowed;
        }
    }
}