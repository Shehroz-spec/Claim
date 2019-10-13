using System;

namespace ClaimDigitalize.Services
{
    public static class StringExtension
    {
        public static string GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static bool IsNullOrEmptyWhiteSpace(this string value)
        {
            return (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()));
        }

        public static string GetDateTimeString(string datetimeString)
        {
            var datetimeResult = string.Empty;
            var splittedDate = new string[datetimeString.Length];

            var splittedDatetime = datetimeString.Split(' ');

            if (splittedDatetime != null && splittedDatetime.Length > 0)
            {
                if (splittedDatetime[0].Contains("/"))
                {
                    splittedDate = splittedDatetime[0].Split('/');
                }
                else if (datetimeString.Contains("-"))
                {
                    splittedDate = splittedDatetime[0].Split('-');
                }

                if (splittedDate.Length > 0)
                {
                    datetimeResult = string.Format("{0} {1}", string.Format("{0}-{1}-{2}", splittedDate[2], splittedDate[1], splittedDate[0]), splittedDatetime[1]);
                }
            }

            return datetimeResult;
        }

        public static string ToDateTimeString(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString)) return dateTimeString;

            var result = Convert.ToDateTime(dateTimeString).ToString("yyyy-MM-dd hh:mm:ss.fff");
            return result;
        }

        public static string ToDateString(object dateTimeObj, bool formatWithTime)
        {
            DateTime? strDate;
            string result = string.Empty;

            if (dateTimeObj != null && !string.IsNullOrEmpty(dateTimeObj.ToString()))
            {
                if (dateTimeObj != "" && formatWithTime)
                {
                    strDate = Convert.ToDateTime(dateTimeObj);
                    result = strDate.Value.ToString("yyyy-MM-dd hh':'mm':'ss");
                }
                else
                {
                    strDate = Convert.ToDateTime(dateTimeObj);
                    result = strDate.Value.ToString("yyyy-MM-dd");
                }
            }

            return result;
        }
    }
}