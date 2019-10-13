using Elmah;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClaimDigitalize
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            //log the error!
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}
