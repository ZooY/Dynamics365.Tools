using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "������������� ������" (list) ��� ���������� GUID.
    /// </summary>
    public class GuidToMarketingList : GuidToEntity
    {
        /// <summary>
        /// ������������� ������.
        /// </summary>
        [Output("Marketing List")]
        [ReferenceTarget(Metadata.MarketingList.LogicalName)]
        public OutArgument<EntityReference> MarketingList { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, MarketingList, Metadata.MarketingList.LogicalName);
        }
    }
}