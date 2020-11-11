using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Обращение" (incident) для указанного GUID.
    /// </summary>
    public class GuidToIncident : GuidToEntity
    {
        /// <summary>
        /// Обращение.
        /// </summary>
        [Output("Incident")]
        [ReferenceTarget("incident")]
        public OutArgument<EntityReference> Incident { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Incident, "incident");
        }
    }
}