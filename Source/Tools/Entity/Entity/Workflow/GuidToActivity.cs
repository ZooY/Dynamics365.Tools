using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Звонок" (phonecall) для указанного GUID.
    /// </summary>
    public class GuidToActivity : GuidToEntity
    {
        /// <summary>
        /// Звонок.
        /// </summary>
        [Output("Phone Call")]
        [ReferenceTarget(Metadata.PhoneCall.LogicalName)]
        public OutArgument<EntityReference> PhoneCall { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, PhoneCall, Metadata.PhoneCall.LogicalName);
        }
    }
}