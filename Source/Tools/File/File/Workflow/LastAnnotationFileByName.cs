using System.Activities;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.FileTools.Workflow
{
    /// <summary>
    /// Получение последнего файла из примечания по имени этого файла.
    /// </summary>
    /// <remarks>
    /// <note type="caution">
    /// Порядок примечаний определяется по дате создания примечания.
    /// </note>
    /// </remarks>
    public class LastAnnotationFileByName : WorkflowBase
    {
        /// <summary>
        /// ID сущности, к которой привязано примечание.
        /// </summary>
        [RequiredArgument]
        [Input("Entity GUID")]
        public InArgument<string> EntityIdString { get; set; }


        /// <summary>
        /// Имя файла вложения примечания.
        /// </summary>
        [RequiredArgument]
        [Input("File Name")]
        public InArgument<string> FileName { get; set; }


        /// <summary>
        /// MIME-тип файла.
        /// </summary>
        [Output("MIME Type")]
        public OutArgument<string> FileMimeType { get; set; }


        /// <summary>
        /// Содержимое файла вложения примечания в формат BASE64.
        /// </summary>
        [Output("File Content (BASE64 Encoding)")]
        public OutArgument<string> FileBase64Content { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var query = @"<fetch top='1' no-lock='true' >
  <entity name='annotation' >
    <attribute name='mimetype' />
    <attribute name='documentbody' />
    <filter>
      <condition attribute='filename' operator='eq' value='" + FileName.Get(context) + @"' />
      <condition attribute='objectid' operator='eq' value='" + EntityIdString.Get(context) + @"' />
    </filter>
    <order attribute='createdon' descending='true' />
  </entity>
</fetch>";
            var response = context.Service.RetrieveMultiple(new FetchExpression(query));
            if (response.Entities.Count < 1)
                return;
            var annotation = response.Entities.First();
            FileMimeType.Set(context, annotation.GetAttributeValue<string>("mimetype"));
            FileBase64Content.Set(context, annotation.GetAttributeValue<string>("documentbody"));
        }
    }
}