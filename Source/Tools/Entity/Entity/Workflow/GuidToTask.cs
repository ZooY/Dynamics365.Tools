using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Задача" (task) для указанного GUID.
    /// </summary>
    public class GuidToTask : GuidToEntity
    {
        /// <summary>
        /// Задача.
        /// </summary>
        [Output("Task")]
        [ReferenceTarget("task")]
        public OutArgument<EntityReference> Task { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Task, "task");
        }
    }
}