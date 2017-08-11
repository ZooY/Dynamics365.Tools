using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�������" (product), "�����-����" (pricelevel), "������� �����-�����" (productpricelevel) � "������� ���������" (uom) ��� ���������� GUID.
    /// </summary>
    public class GuidToProduct : GuidToEntity
    {
        /// <summary>
        /// �������.
        /// </summary>
        [Output("Product")]
        [ReferenceTarget(Metadata.Product.LogicalName)]
        public OutArgument<EntityReference> Product { get; set; }


        /// <summary>
        /// �����-����.
        /// </summary>
        [Output("Price List")]
        [ReferenceTarget(Metadata.PriceList.LogicalName)]
        public OutArgument<EntityReference> PriceList { get; set; }


        /// <summary>
        /// ������� �����-�����.
        /// </summary>
        [Output("Price List Product")]
        [ReferenceTarget(Metadata.PriceListProduct.LogicalName)]
        public OutArgument<EntityReference> PriceListItem { get; set; }


        /// <summary>
        /// ������� ���������.
        /// </summary>
        [Output("Unit")]
        [ReferenceTarget(Metadata.Unit.LogicalName)]
        public OutArgument<EntityReference> Unit { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Product, Metadata.Product.LogicalName);
            SetValue(context, PriceList, Metadata.PriceList.LogicalName);
            SetValue(context, PriceListItem, Metadata.PriceListProduct.LogicalName);
            SetValue(context, Unit, Metadata.Unit.LogicalName);
        }
    }
}