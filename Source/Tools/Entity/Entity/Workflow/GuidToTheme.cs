using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Тема" (theme) для указанного GUID.
    /// </summary>
    public class GuidToTheme : GuidToEntity
    {
        /// <summary>
        /// Тема.
        /// </summary>
        [Output("Theme")]
        [ReferenceTarget("theme")]
        public OutArgument<EntityReference> Theme { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Theme, "theme");
        }
    }
}