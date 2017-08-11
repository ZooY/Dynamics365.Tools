using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "������" (phonecall) ��� ���������� GUID.
    /// </summary>
    public class GuidToActivity : GuidToEntity
    {
        /// <summary>
        /// ������.
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