using System;
using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;
using PZone.Xrm.Sdk;


namespace PZone.FetchXmlTools.Workflow
{
    public abstract class AssignOwner : FetchXmlWorkflow
    {
        /// <summary>
        /// Количество назначенных записей.
        /// </summary>
        [Output("Affected Entities Count")]
        public OutArgument<int> AffectedEntities { get; set; }


        protected void Execute(Context context, EntityReference ownerRef)
        {
            var fetchXml = FetchXml.Get(context);
            var entities = context.Service.RetrieveMultiple(fetchXml).Entities;
            var affectedEntities = 0;
            foreach (var entity in entities)
            {
                try
                {
                    var request = new AssignRequest { Assignee = ownerRef, Target = entity.ToEntityReference() };
                    context.Service.Execute(request);
                    affectedEntities++;
                }
                catch (Exception ex)
                {
                    context.TracingService.Trace(ex);
                }
            }
            if (affectedEntities == 0)
                SetError(context, "В процессе назначения записей произошли ошибки. Ни одна запись не назначена.");
            else if (affectedEntities < entities.Count)
                SetError(context, $"В процессе назначения записей произошли ошибки. Записей назначено: {affectedEntities}, записей не назначено: {entities.Count - affectedEntities}.");
            AffectedEntities.Set(context, affectedEntities);
        }
    }
}