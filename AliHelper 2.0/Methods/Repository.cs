using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AliHelper_2._0.Methods
{
    public abstract class Repository
    {
        public static string GetTokenWithClient(string SsidToken, string ClientID, string ClientSecret)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://oauth2.epn.bz/token");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-API-VERSION", "2");
            httpWebRequest.Headers.Add("X-SSID", SsidToken);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{  \"grant_type\": \"client_credential\",\"client_id\": \"" + ClientID + "\",\"client_secret\": \"" + ClientSecret + "\"}";

                streamWriter.Write(json);
            }

            string Result = "";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Result = result;
                }
            }
            catch (WebException exception)
            {
                MessageBox.Show("Неправильные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(StreamToString(exception.Response.GetResponseStream()));
            }
            return Result;
        }

        public static string StreamToString(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static string Get(Uri Url)
        {
            try
            {
                HttpWebRequest Request = WebRequest.CreateHttp(Url);
                Request.Method = "GET";
                Request.Headers.Add("X-API-VERSION", "2");
                using (WebResponse Response = Request.GetResponse())
                {
                    StreamReader reader = new StreamReader(Response.GetResponseStream());
                    var Respons = reader.ReadToEnd();

                    Console.WriteLine(Respons);

                    return Respons;
                }
            }
            catch (WebException exception)
            {
                Console.WriteLine(StreamToString(exception.Response.GetResponseStream()));
            }
            return "";
        }

        public string CreateOauthClient(string AccessToken)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://app.epn.bz/user/oauth/client");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-ACCESS-TOKEN", AccessToken);


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
            }

            string Result = "";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Result = result;
                }
            }
            catch (WebException exception)
            {
                MessageBox.Show("Неправильные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(StreamToString(exception.Response.GetResponseStream()));
            }
            return Result;
        }
        public static string GetTokenWithLogin(string SsidToken, string Login, string Password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://oauth2.epn.bz/token");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-API-VERSION", "2");
            httpWebRequest.Headers.Add("X-SSID", SsidToken);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{  \"grant_type\": \"password \",\"username\": \"" + Login + "\",\"password\": \"" + Password + "\",\"client_id\":\"web-client\"}";

                streamWriter.Write(json);
            }

            string Result = "";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Result = result;
                }
            }
            catch (WebException exception)
            {
                MessageBox.Show("Неправильные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(StreamToString(exception.Response.GetResponseStream()));
            }
            return Result;
        }

    }
}
