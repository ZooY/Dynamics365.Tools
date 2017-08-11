using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Интерес" (lead) для указанного GUID.
    /// </summary>
    public class GuidToLead : GuidToEntity
    {
        /// <summary>
        /// Интерес.
        /// </summary>
        [Output("Lead")]
        [ReferenceTarget(Metadata.Lead.LogicalName)]
        public OutArgument<EntityReference> Lead { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Lead, Metadata.Lead.LogicalName);
        }
    }
}