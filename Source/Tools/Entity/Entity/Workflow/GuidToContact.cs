using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�������" (contact) ��� ���������� GUID.
    /// </summary>
    public class GuidToContact : GuidToEntity
    {
        /// <summary>
        /// �������.
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