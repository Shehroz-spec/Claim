using ClaimDigitalize.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace ClaimDigitalize.Services
{
    public static class HttpExtentions
    {
        private static readonly HttpClient client = new HttpClient();

        public static string GetRequest(string BaseUrl, string headers = "")
        {
            string result = string.Empty;

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.MaximumResponseHeadersLength = 2147483647;
                httpWebRequest.KeepAlive = false;

                if (!string.IsNullOrEmpty(headers))
                {
                    httpWebRequest.Headers.Add("Authorization", headers);
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return result;
        }

        public static string PostRequest(string BaseUrl, object data)
        {
            string result = string.Empty;

            try
            {
                string json = new JavaScriptSerializer().Serialize(data);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                httpWebRequest.MaximumResponseHeadersLength = 2147483647;
                httpWebRequest.KeepAlive = false;

                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return result;
        }

        public static T PostRequest<T>(string baseUrl, object data, string headers = "") where T : class, new()
        {
            string result = string.Empty;

            try
            {
                string json = JsonConvert.SerializeObject(data);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                httpWebRequest.MaximumResponseHeadersLength = 2147483647;
                httpWebRequest.KeepAlive = true;

                if (!string.IsNullOrEmpty(headers))
                {
                    httpWebRequest.Headers.Add("Authorization", headers);
                }

                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public static string Post(string baseUrl, LoginVM loginModel)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            string postData = string.Format("username={0}&password={1}&grant_type={2}",
                loginModel.UserName, loginModel.Password, loginModel.GrantType);
            byte[] data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            string responseString = string.Empty;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                if (httpResponse.StatusCode != HttpStatusCode.Unauthorized)
                {
                    responseString = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return responseString;
        }
    }
}