using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Подразделение" (businessunit) для указанного GUID.
    /// </summary>
    public class GuidToBusinessUnit : GuidToEntity
    {
        /// <summary>
        /// Подразделение.
        /// </summary>
        [Output("Business Unit")]
        [ReferenceTarget(Metadata.BusinessUnit.LogicalName)]
        public OutArgument<EntityReference> BusinessUnit { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, BusinessUnit, Metadata.BusinessUnit.LogicalName);
        }
    }
}