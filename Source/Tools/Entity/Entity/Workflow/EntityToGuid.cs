using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;

namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// The action returns the GUID of the specified entity.
    /// </summary>
    public abstract class EntityToGuid : WorkflowBase
    {
        /// <summary>
        /// GUID.
        /// </summary>
        [Output("GUID")]
        public OutArgument<string> Guid { get; set; }
    }
}