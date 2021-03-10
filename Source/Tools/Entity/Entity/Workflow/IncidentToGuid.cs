using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Getting the GUID of the Incident entity.
    /// </summary>
    public class IncidentToGuid : EntityToGuid
    {
        /// <summary>
        /// Incident.
        /// </summary>
        [Input("Incident")]
        [ReferenceTarget("incident")]
        public InArgument<EntityReference> Incident { get; set; }


        protected override void Execute(Context context)
        {
            Guid.Set(context, Incident.Get(context).Id.ToString().Replace("{", "").Replace("}", ""));
        }
    }
}