using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.LookupTools.Workflow
{
    /// <summary>
    /// Нормализация ссылки на сущность. Заключается в преобразовании ссылки с нулевым идентификатором в <c>null</c>.
    /// </summary>
    public class Normalization : WorkflowBase
    {
        /// <summary>
        /// Ссылка на запись пользователя.
        /// </summary>
        [RequiredArgument]
        [Input("System User")]
        [Output("Normalized System User")]
        [ReferenceTarget("systemuser")]
        public InOutArgument<EntityReference> SystemUserRef { get; set; }


        /// <summary>
        /// Ссылка на запись организации.
        /// </summary>
        [RequiredArgument]
        [Input("Account")]
        [Output("Normalized Account")]
        [ReferenceTarget("account")]
        public InOutArgument<EntityReference> AccountRef { get; set; }


        /// <summary>
        /// Ссылка на запись персоны.
        /// </summary>
        [RequiredArgument]
        [Input("Contact")]
        [Output("Normalized Contact")]
        [ReferenceTarget("contact")]
        public InOutArgument<EntityReference> ContactRef { get; set; }


        /// <summary>
        /// Ссылка на запись интерса.
        /// </summary>
        [RequiredArgument]
        [Input("Lead")]
        [Output("Normalized Lead")]
        [ReferenceTarget("lead")]
        public InOutArgument<EntityReference> LeadRef { get; set; }
        

        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            NormalizeEntity(context, SystemUserRef);
            NormalizeEntity(context, AccountRef);
            NormalizeEntity(context, ContactRef);
            NormalizeEntity(context, LeadRef);
        }



        private static void NormalizeEntity(Context context, InOutArgument<EntityReference> entityRefArg)
        {
            var entityRef = entityRefArg.Get(context);
            entityRefArg.Set(context, entityRef == null || entityRef.Id == Guid.Empty ? null : entityRef);
        }


    }
}