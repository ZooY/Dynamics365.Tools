using System;
using PZone.Common.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Удаление текущей записи.
    /// </summary>
    public class DeleteCurrent : WorkflowBase
    {
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            if (string.IsNullOrEmpty(context.EntityName) || context.EntityId == Guid.Empty)
                return;
            context.Service.Delete(context.EntityName, context.EntityId);
        }
    }
}