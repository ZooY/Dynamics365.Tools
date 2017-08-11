using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "����" (invoice) � "������� �����" (invoicedetail) ��� ���������� GUID.
    /// </summary>
    public class GuidToInvoice : GuidToEntity
    {
        /// <summary>
        /// ����.
        /// </summary>
        [Output("Invoice")]
        [ReferenceTarget(Metadata.Invoice.LogicalName)]
        public OutArgument<EntityReference> Invoice { get; set; }


        /// <summary>
        /// ������� �����.
        /// </summary>
        [Output("Invoice Product")]
        [ReferenceTarget(Metadata.InvoiceProduct.LogicalName)]
        public OutArgument<EntityReference> InvoiceProduct { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Invoice, Metadata.Invoice.LogicalName);
            SetValue(context, InvoiceProduct, Metadata.InvoiceProduct.LogicalName);
        }
    }
}