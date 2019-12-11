using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Добавление интереса в маркетинговый список.
    /// </summary>
    public class AddLeadToList : AddEntityToList
    {
        /// <summary>
        /// Интерес.
        /// </summary>
        [RequiredArgument]
        [Input("Lead")]
        [ReferenceTarget("lead")]
        public InArgument<EntityReference> LeadRef { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            AddToList(context, LeadRef.Get(context).Id);
        }
    }
}