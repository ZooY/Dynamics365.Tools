using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущности "Предложение" (quote) и "Продукт предложения" (quotedetail) для указанного GUID.
    /// </summary>
    public class GuidToQuote : GuidToEntity
    {
        /// <summary>
        /// Предложение.
        /// </summary>
        [Output("Quote")]
        [ReferenceTarget(Metadata.Quote.LogicalName)]
        public OutArgument<EntityReference> Quote { get; set; }


        /// <summary>
        /// Продукт предложения.
        /// </summary>
        [Output("Quote Product")]
        [ReferenceTarget(Metadata.QuoteProduct.LogicalName)]
        public OutArgument<EntityReference> QuoteProduct { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Quote, Metadata.Quote.LogicalName);
            SetValue(context, QuoteProduct, Metadata.QuoteProduct.LogicalName);
        }
    }
}