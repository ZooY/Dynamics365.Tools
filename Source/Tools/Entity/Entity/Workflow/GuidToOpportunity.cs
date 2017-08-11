using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "��������� ������" (opportunity) � "������� �������� ������" (opportunityproduct) ��� ���������� GUID.
    /// </summary>
    public class GuidToOpportunity : GuidToEntity
    {
        /// <summary>
        /// ��������� ������.
        /// </summary>
        [Output("Opportunity")]
        [ReferenceTarget(Metadata.Opportunity.LogicalName)]
        public OutArgument<EntityReference> Opportunity { get; set; }


        /// <summary>
        /// ������� �������� ������.
        /// </summary>
        [Output("Opportunity Product")]
        [ReferenceTarget(Metadata.OpportunityProduct.LogicalName)]
        public OutArgument<EntityReference> OpportunityProduct { get; set; }



        protected override void Execute(Context context)
        {
            SetValue(context, Opportunity, Metadata.Opportunity.LogicalName);
            SetValue(context, OpportunityProduct, Metadata.OpportunityProduct.LogicalName);
        }
    }
}