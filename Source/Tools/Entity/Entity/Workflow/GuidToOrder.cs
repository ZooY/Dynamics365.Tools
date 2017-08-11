using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�����" (salesorder) � "������� ������" (salesorderdetail) ��� ���������� GUID.
    /// </summary>
    public class GuidToOrder : GuidToEntity
    {
        /// <summary>
        /// �����.
        /// </summary>
        [Output("Order")]
        [ReferenceTarget(Metadata.Order.LogicalName)]
        public OutArgument<EntityReference> Order { get; set; }


        /// <summary>
        /// ������� ������.
        /// </summary>
        [Output("Order Product")]
        [ReferenceTarget(Metadata.OrderProduct.LogicalName)]
        public OutArgument<EntityReference> OrderProduct { get; set; }



        protected override void Execute(Context context)
        {
            SetValue(context, Order, Metadata.Order.LogicalName);
            SetValue(context, OrderProduct, Metadata.OrderProduct.LogicalName);
        }
    }
}