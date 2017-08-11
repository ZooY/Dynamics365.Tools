using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�����������" (account) ��� ���������� GUID.
    /// </summary>
    public class GuidToAccount : GuidToEntity
    {
        /// <summary>
        /// �����������.
        /// </summary>
        [Output("Account")]
        [ReferenceTarget(Metadata.Account.LogicalName)]
        public OutArgument<EntityReference> Account { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Account, Metadata.Account.LogicalName);
        }
    }
}