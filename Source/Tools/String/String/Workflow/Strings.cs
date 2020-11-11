using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Специальные строки.
    /// </summary>
    public class Strings : WorkflowBase
    {
        /// <summary>
        /// Случайный GUID.
        /// </summary>
        [Output("GUID")]
        public OutArgument<string> Guid { get; set; }

        
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Guid.Set(context, System.Guid.NewGuid().ToString());
        }
    }
}