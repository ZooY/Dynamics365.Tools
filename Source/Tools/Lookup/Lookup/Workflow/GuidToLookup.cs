using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Common.Workflow;
using PZone.Common.Workflow.Exceptions;
using PZone.Activities;

namespace PZone.LookupTools.Workflow
{
    /// <summary>
    /// Дейсвтие возвращает Lookup для указанного GUID.
    /// Возвращаемые типы сущностей:
    /// - Account (account)
    /// - Contact (contact)
    /// - Business Unit (businessunit)
    /// - Currency (transactioncurrency)
    /// - Invoice (invoice)
    /// - Invoice Product (invoicedetail)
    /// - Lead (lead)
    /// - Marketing List (list)
    /// - Opportunity (opportunity)
    /// - Opportunity Product (opportunityproduct)
    /// - Order (salesorder)
    /// - Order Product (salesorderdetail)
    /// - Price List (pricelevel)
    /// - Price List Item (productpricelevel)
    /// - Workflow (workflow)
    /// - Product (product)
    /// - Queue (queue)
    /// - Queue Item (queueitem)
    /// - Quote (quote)
    /// - Quote Product (quotedetail)
    /// - User (systemuser)
    /// - Unit (uom)
    /// - Team (team)
    /// - Phone Call (phonecall)
    /// </summary>
    public class GuidToLookup : WorkflowBase
    {
        /// <summary>
        /// GUID в виде строки.
        /// </summary>
        [RequiredArgument]
        [Input("GUID String")]
        public InArgument<string> GuidString { get; set; }


        /// <summary>
        /// Организация.
        /// </summary>
        [Output("Account Lookup")]
        [ReferenceTarget("account")]
        public OutArgument<EntityReference> AccountRef { get; set; }


        /// <summary>
        /// Персона.
        /// </summary>
        [Output("Contact Lookup")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> ContactRef { get; set; }


        /// <summary>
        /// Подразделение.
        /// </summary>
        [Output("Business Unit Lookup")]
        [ReferenceTarget("businessunit")]
        public OutArgument<EntityReference> BusinessUnitRef { get; set; }


        /// <summary>
        /// Валюта.
        /// </summary>
        [Output("Currency Lookup")]
        [ReferenceTarget("transactioncurrency")]
        public OutArgument<EntityReference> CurrencyRef { get; set; }


        /// <summary>
        /// Счет.
        /// </summary>
        [Output("Invoice Lookup")]
        [ReferenceTarget("invoice")]
        public OutArgument<EntityReference> InvoiceRef { get; set; }


        /// <summary>
        /// Продукт счета.
        /// </summary>
        [Output("Invoice Product Lookup")]
        [ReferenceTarget("invoicedetail")]
        public OutArgument<EntityReference> InvoiceProductRef { get; set; }


        /// <summary>
        /// Интерес.
        /// </summary>
        [Output("Lead Lookup")]
        [ReferenceTarget("lead")]
        public OutArgument<EntityReference> LeadRef { get; set; }


        /// <summary>
        /// Маркетинговый список.
        /// </summary>
        [Output("Marketing List Lookup")]
        [ReferenceTarget("list")]
        public OutArgument<EntityReference> MarketingListRef { get; set; }


        /// <summary>
        /// Возможная сделка.
        /// </summary>
        [Output("Opportunity Lookup")]
        [ReferenceTarget("opportunity")]
        public OutArgument<EntityReference> OpportunityRef { get; set; }


        /// <summary>
        /// Продукт возможой сделки.
        /// </summary>
        [Output("Opportunity Product Lookup")]
        [ReferenceTarget("opportunityproduct")]
        public OutArgument<EntityReference> OpportunityProductRef { get; set; }


        /// <summary>
        /// Заказ.
        /// </summary>
        [Output("Order Lookup")]
        [ReferenceTarget("salesorder")]
        public OutArgument<EntityReference> OrderRef { get; set; }


        /// <summary>
        /// Продукт заказа.
        /// </summary>
        [Output("Order Product Lookup")]
        [ReferenceTarget("salesorderdetail")]
        public OutArgument<EntityReference> OrderProductRef { get; set; }


        /// <summary>
        /// Прайс-лист.
        /// </summary>
        [Output("Price List Lookup")]
        [ReferenceTarget("pricelevel")]
        public OutArgument<EntityReference> PriceListRef { get; set; }


        /// <summary>
        /// Продукт прайс-листа.
        /// </summary>
        [Output("Price List Item Lookup")]
        [ReferenceTarget("productpricelevel")]
        public OutArgument<EntityReference> PriceListItemRef { get; set; }


        /// <summary>
        /// Workflow.
        /// </summary>
        [Output("Workflow Lookup")]
        [ReferenceTarget("workflow")]
        public OutArgument<EntityReference> WorkflowRef { get; set; }


        /// <summary>
        /// Продукт.
        /// </summary>
        [Output("Product Lookup")]
        [ReferenceTarget("product")]
        public OutArgument<EntityReference> ProductRef { get; set; }


        /// <summary>
        /// Очередь.
        /// </summary>
        [Output("Queue Lookup")]
        [ReferenceTarget("queue")]
        public OutArgument<EntityReference> QueueRef { get; set; }


        /// <summary>
        /// Элемент очереди.
        /// </summary>
        [Output("Queue Item Lookup")]
        [ReferenceTarget("queueitem")]
        public OutArgument<EntityReference> QueueItemRef { get; set; }


        /// <summary>
        /// Предложение.
        /// </summary>
        [Output("Quote Lookup")]
        [ReferenceTarget("quote")]
        public OutArgument<EntityReference> QuoteRef { get; set; }


        /// <summary>
        /// Продукт предложения.
        /// </summary>
        [Output("Quote Product Lookup")]
        [ReferenceTarget("quotedetail")]
        public OutArgument<EntityReference> QuoteProductRef { get; set; }


        /// <summary>
        /// Пользователь.
        /// </summary>
        [Output("User Lookup")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> UserRef { get; set; }


        /// <summary>
        /// Единица изменения.
        /// </summary>
        [Output("Unit Lookup")]
        [ReferenceTarget("uom")]
        public OutArgument<EntityReference> UnitRef { get; set; }


        /// <summary>
        /// Группа пользователей.
        /// </summary>
        [Output("Team Lookup")]
        [ReferenceTarget("team")]
        public OutArgument<EntityReference> TeamRef { get; set; }


        /// <summary>
        /// Звонок.
        /// </summary>
        [Output("Phone Call")]
        [ReferenceTarget("phonecall")]
        public OutArgument<EntityReference> PhoneCallRef { get; set; }


        protected override void Execute(Context context)
        {
            var guidString = GuidString.Get(context);
            Guid guid;
            try
            {
                guid = Guid.Parse(guidString);
            }
            catch (Exception ex)
            {
                throw new InvalidWorkflowExecutionException($"Значение \"{guidString}\" не может быть приведено к типу GUID. ", ex);
            }
            AccountRef.Set(context, new EntityReference("account", guid));
            ContactRef.Set(context, new EntityReference("contact", guid));
            BusinessUnitRef.Set(context, new EntityReference("businessunit", guid));
            CurrencyRef.Set(context, new EntityReference("transactioncurrency", guid));
            InvoiceRef.Set(context, new EntityReference("invoice", guid));
            InvoiceProductRef.Set(context, new EntityReference("invoicedetail", guid));
            LeadRef.Set(context, new EntityReference("lead", guid));
            MarketingListRef.Set(context, new EntityReference("list", guid));
            OpportunityRef.Set(context, new EntityReference("opportunity", guid));
            OpportunityProductRef.Set(context, new EntityReference("opportunityproduct", guid));
            OrderRef.Set(context, new EntityReference("salesorder", guid));
            OrderProductRef.Set(context, new EntityReference("salesorderdetail", guid));
            PriceListRef.Set(context, new EntityReference("pricelevel", guid));
            PriceListItemRef.Set(context, new EntityReference("productpricelevel", guid));
            WorkflowRef.Set(context, new EntityReference("workflow", guid));
            ProductRef.Set(context, new EntityReference("product", guid));
            QueueRef.Set(context, new EntityReference("queue", guid));
            QueueItemRef.Set(context, new EntityReference("queueitem", guid));
            QuoteRef.Set(context, new EntityReference("quote", guid));
            QuoteProductRef.Set(context, new EntityReference("quotedetail", guid));
            UserRef.Set(context, new EntityReference("systemuser", guid));
            UnitRef.Set(context, new EntityReference("uom", guid));
            TeamRef.Set(context, new EntityReference("team", guid));
            PhoneCallRef.Set(context, new EntityReference("phonecall", guid));
        }
    }
}