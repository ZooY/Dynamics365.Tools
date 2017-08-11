using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Бизнес-процесс" (workflow) для указанного GUID.
    /// </summary>
    public class GuidToWorkflow : GuidToEntity
    {
        /// <summary>
        /// Бизнес-процесс.
        /// </summary>
        [Output("Workflow")]
        [ReferenceTarget(Metadata.Workflow.LogicalName)]
        public OutArgument<EntityReference> Workflow { get; set; }



        protected override void Execute(Context context)
        {
            SetValue(context, Workflow, Metadata.Workflow.LogicalName);
        }
    }
}