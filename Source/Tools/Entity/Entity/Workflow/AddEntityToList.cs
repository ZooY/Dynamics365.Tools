using System;
using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    // Базовый класс для добавления элементов маркетингового списка.
    /// </summary>
    public abstract class AddEntityToList : WorkflowBase
    {
        /// <summary>
        /// Маркетинговый список.
        /// </summary>
        [RequiredArgument]
        [Input("Marketing List")]
        [ReferenceTarget("list")]
        public InArgument<EntityReference> ListRef { get; set; }


        protected void AddToList(Context context, Guid entityId)
        {
            var service = context.SourceActivityContext.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(null);
            service.Execute(new AddListMembersListRequest
            {
                ListId = ListRef.Get(context).Id,
                MemberIds = new[] { entityId }
            });
        }
    }
}