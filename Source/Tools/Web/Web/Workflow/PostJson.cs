using System;
using System.Activities;
using System.Net;
using System.Text;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.WebTools.Workflow
{
    /// <summary>
    /// Выполнение POST-запроса к удаленному веб-сервису и отправка данных в формате JSON.
    /// </summary>
    public class PostJson : WorkflowBase
    {
        /// <summary>
        /// Адрес удаленного веб-сервиса.
        /// </summary>
        [Input("Service URL")]
        [RequiredArgument]
        public InArgument<string> Url { get; set; }


        /// <summary>
        /// Строка в формате JSON.
        /// </summary>
        [Input("JSON")]
        public InArgument<string> Json { get; set; }


        /// <summary>
        /// Содержимое ответа веб-сервиса.
        /// </summary>
        [Output("Response")]
        public OutArgument<string> Response { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var url = Url.Get(context);
            var json = Json.Get(context);
            using (var client = new ExtendedWebClient())
            {
                var response = client.UploadString(url, json ?? string.Empty);
                Response.Set(context, response);
            }
        }


        /// <inheritdoc />
        protected class ExtendedWebClient : WebClient
        {
            public ExtendedWebClient()
            {
                Encoding = Encoding.UTF8;
                Headers.Add(HttpRequestHeader.ContentType, "application/json");
            }


            protected override WebRequest GetWebRequest(Uri address)
            {
                var w = base.GetWebRequest(address);
                w.Timeout = int.MaxValue;
                return w;
            }
        }
    }
}