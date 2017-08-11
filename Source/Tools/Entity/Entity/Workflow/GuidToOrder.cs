using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущности "Заказ" (salesorder) и "Продукт заказа" (salesorderdetail) для указанного GUID.
    /// </summary>
    public class GuidToOrder : GuidToEntity
    {
        /// <summary>
        /// Заказ.
        /// </summary>
        [Output("Order")]
        [ReferenceTarget(Metadata.Order.LogicalName)]
        public OutArgument<EntityReference> Order { get; set; }


        /// <summary>
        /// Продукт заказа.
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