using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Получение ID текущей записи.
    /// </summary>
    public class CurrentEntityId : WorkflowBase
    {

        /// <summary>
        /// ID текущей записи в виде строки.
        /// </summary>
        [Output("Current Entity ID")]
        public OutArgument<string> EntityId { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            if (context.EntityId != default(Guid))
                EntityId.Set(context, context.EntityId.ToString());
        }
    }
}