using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "E-mail" (email) для указанного GUID.
    /// </summary>
    public class GuidToEmail : GuidToEntity
    {
        /// <summary>
        /// E-mail.
        /// </summary>
        [Output("E-mail")]
        [ReferenceTarget(Metadata.Email.LogicalName)]
        public OutArgument<EntityReference> Email { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Email, Metadata.Email.LogicalName);
        }
    }
}