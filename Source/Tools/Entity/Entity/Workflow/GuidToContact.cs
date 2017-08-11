using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Персона" (contact) для указанного GUID.
    /// </summary>
    public class GuidToContact : GuidToEntity
    {
        /// <summary>
        /// Персона.
        /// </summary>
        [Output("Contact")]
        [ReferenceTarget(Metadata.Contact.LogicalName)]
        public OutArgument<EntityReference> Contact { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Contact, Metadata.Contact.LogicalName);
        }
    }
}