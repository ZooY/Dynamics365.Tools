using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность для указанного GUID.
    /// </summary>
    public abstract class GuidToEntity : WorkflowBase
    {
        /// <summary>
        /// GUID в виде строки.
        /// </summary>
        [RequiredArgument]
        [Input("GUID String")]
        public InArgument<string> GuidString { get; set; }


        protected void SetValue(Context context, OutArgument<EntityReference> outArgument, string entityName)
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
            if (guid == Guid.Empty)
                throw new InvalidWorkflowExecutionException($"Значение \"{guidString}\" не может быть пустым GUID.");
            outArgument.Set(context, new EntityReference(entityName, guid));
        }
    }
}