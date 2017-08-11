using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�����������" (quote) � "������� �����������" (quotedetail) ��� ���������� GUID.
    /// </summary>
    public class GuidToQuote : GuidToEntity
    {
        /// <summary>
        /// �����������.
        /// </summary>
        [Output("Quote")]
        [ReferenceTarget(Metadata.Quote.LogicalName)]
        public OutArgument<EntityReference> Quote { get; set; }


        /// <summary>
        /// ������� �����������.
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