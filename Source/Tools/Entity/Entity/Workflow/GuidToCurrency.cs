using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Валюта" (transactioncurrency) для указанного GUID.
    /// </summary>
    public class GuidToCurrency : GuidToEntity
    {
        /// <summary>
        /// Валюта.
        /// </summary>
        [Output("Currency")]
        [ReferenceTarget(Metadata.Currency.LogicalName)]
        public OutArgument<EntityReference> Currency { get; set; }



        protected override void Execute(Context context)
        {
            SetValue(context, Currency, Metadata.Currency.LogicalName);
        }
    }
}