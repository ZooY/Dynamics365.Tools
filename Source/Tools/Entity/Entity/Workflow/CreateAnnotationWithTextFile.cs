using System;
using System.Activities;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Создание примечания с вложением текстового файла для произвольной сущности.
    /// </summary>
    public class CreateAnnotationWithTextFile : WorkflowBase
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        [RequiredArgument]
        [Input("Entity Logical Name")]
        public InArgument<string> EntityLogicalName { get; set; }


        /// <summary>
        /// ID сущности.
        /// </summary>
        [RequiredArgument]
        [Input("Entity GUID")]
        public InArgument<string> EntityIdString { get; set; }


        /// <summary>
        /// Тема примечания.
        /// </summary>
        [Input("Subject")]
        public InArgument<string> Subject { get; set; }


        /// <summary>
        /// Текстовое содердимое примечания.
        /// </summary>
        [Input("Text")]
        public InArgument<string> Content { get; set; }


        /// <summary>
        /// Имя файла вложения примечания.
        /// </summary>
        [Input("File Name")]
        public InArgument<string> FileName { get; set; }


        /// <summary>
        /// MIME-тип вложения примечания.
        /// </summary>
        [Input("File MIME Type")]
        [Default("text/plain")]
        public InArgument<string> FileMimeType { get; set; }


        /// <summary>
        /// Содержимое файла вложения примечания в формат BASE64.
        /// </summary>
        [Input("File Content")]
        public InArgument<string> FileContent { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var entityId = new Guid(EntityIdString.Get(context));
            var entity = new Entity(Metadata.Annotation.LogicalName)
            {
                ["objectid"] = new EntityReference(EntityLogicalName.Get(context), entityId),
                ["subject"] = Subject.Get(context),
                ["notetext"] = Content.Get(context)
            };
            var fileName = FileName.Get(context);
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var content = FileContent.Get(context);
                entity["filename"] = fileName;
                entity["mimetype"] = FileMimeType.Get(context);
                entity["documentbody"] = string.IsNullOrWhiteSpace(content) ? "" : Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
            }
            context.Service.Create(entity);
        }
    }
}