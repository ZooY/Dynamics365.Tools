using System;
using System.Activities;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.FileTools.Workflow
{
    /// <summary>
    /// Получение содержимого файла веб-ресурса.
    /// </summary>
    public class WebResource : WorkflowBase
    {
        /// <summary>
        /// Уникальное имя веб-ресурса.
        /// </summary>
        [RequiredArgument]
        [Input("WebResource Name")]
        public InArgument<string> WebResourceName { get; set; }


        /// <summary>
        /// Содержимое файла веб-ресурса в формат BASE64.
        /// </summary>
        [Output("Content in BASE64 Encoding")]
        public OutArgument<string> Base64Content { get; set; }


        /// <summary>
        /// Содержимое файла веб-ресурса в виде строки.
        /// </summary>
        [Output("Content as String")]
        public OutArgument<string> StringContent { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var name = WebResourceName.Get(context);
            var webResource = context.Service.RetrieveMultiple(new FetchExpression($@"<fetch top=""1"" no-lock=""true"" >
  <entity name=""webresource"" >
    <attribute name=""content"" />
    <filter>
      <condition attribute=""name"" operator=""eq"" value=""{name}"" />
    </filter>
  </entity>
</fetch>")).Entities.FirstOrDefault();
            if (webResource == null)
                throw new Exception($"Веб-ресурс с именем \"{name}\" не найден.");
            var content = webResource.GetAttributeValue<string>("content");
            Base64Content.Set(context, content);
            StringContent.Set(context, Encoding.UTF8.GetString(Convert.FromBase64String(content)));
        }
    }
}