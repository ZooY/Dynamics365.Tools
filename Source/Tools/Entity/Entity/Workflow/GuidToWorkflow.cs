using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "������-�������" (workflow) ��� ���������� GUID.
    /// </summary>
    public class GuidToWorkflow : GuidToEntity
    {
        /// <summary>
        /// ������-�������.
        /// </summary>
        [Output("Workflow")]
        [ReferenceTarget(Metadata.Workflow.LogicalName)]
        public OutArgument<EntityReference> Workflow { get; set; }



        protected override void Execute(Context context)
        {
            SetValue(context, Workflow, Metadata.Workflow.LogicalName);
        }
    }
}