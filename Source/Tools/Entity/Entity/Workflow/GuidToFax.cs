using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Факс" (fax) для указанного GUID.
    /// </summary>
    public class GuidToFax : GuidToEntity
    {
        /// <summary>
        /// Факс.
        /// </summary>
        [Output("Fax")]
        [ReferenceTarget("fax")]
        public OutArgument<EntityReference> Fax { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Fax, "fax");
        }
    }
}