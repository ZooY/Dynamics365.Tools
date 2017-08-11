using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Специальные символы.
    /// </summary>
    public class Characters : WorkflowBase
    {
        /// <summary>
        /// Пробел.
        /// </summary>
        [Output("Space")]
        public OutArgument<string> Space { get; set; }


        /// <summary>
        /// Перевод строки.
        /// </summary>
        [Output("New Line")]
        public OutArgument<string> NewLine { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Space.Set(context, " ");
            NewLine.Set(context, Environment.NewLine);
        }
    }
}