using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Маркетинговый список" (list) для указанного GUID.
    /// </summary>
    public class GuidToMarketingList : GuidToEntity
    {
        /// <summary>
        /// Маркетинговый список.
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