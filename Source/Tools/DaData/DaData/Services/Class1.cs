//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Web;
//using Microsoft.Xrm.Sdk;
//using Newtonsoft.Json;


//namespace PZone.DaDataTools.Services
//{
//    public class DaDataService
//    {
//        /// <summary>
//        /// Ссылка на сервис CRM.
//        /// </summary>
//        protected readonly IOrganizationService Service;


//        private readonly Lazy<DaDataSettings> _settings;


//        /// <summary>
//        /// Конструтор класса.
//        /// </summary>
//        /// <param name="service">Ссылка на сервис CRM.</param>
//        public DaDataService(IOrganizationService service)
//        {
//            Service = service;
//            _settings = new Lazy<DaDataSettings>(() => new DaDataSettingsService(Service).Retrieve());
//        }


//        public string RetrieveAddresses(string query, int count)
//        {
//            query = new Regex(@"[\\""']").Replace(query, "");
//            HttpWebRequest request;
//            if (_settings.Value.UseProxy)
//            {
//                request = WebRequest.CreateHttp($@"{_settings.Value.AddressesUrl}?query={HttpUtility.UrlEncode(query)}&count={count}");
//                request.Method = "GET";
//                if (_settings.Value.UseAuthentication)
//                    request.UseDefaultCredentials = true;
//            }
//            else
//            {
//                var data = JsonConvert.SerializeObject(new { query = query, count = count });
//                var postData = Encoding.UTF8.GetBytes(data);
//                request = WebRequest.CreateHttp(_settings.Value.AddressesUrl);
//                request.Method = "POST";
//                request.ContentType = "application/json";
//                request.Accept = "application/json";
//                request.Headers.Add(HttpRequestHeader.Authorization, $@"Token {_settings.Value.ApiKey}");
//                request.ContentLength = postData.Length;
//                using (var dataStream = request.GetRequestStream())
//                {
//                    dataStream.Write(postData, 0, postData.Length);
//                }
//            }
//            string json;
//            using (var response = request.GetResponse())
//            {
//                var responseStream = response.GetResponseStream();
//                if (responseStream == null)
//                    throw new Exception("Сервис DaData вернул пустой ответ.");
//                using (var streamReader = new StreamReader(responseStream))
//                {
//                    json = streamReader.ReadToEnd();
//                }
//            }
//            return json;
//        }
//    }
//}