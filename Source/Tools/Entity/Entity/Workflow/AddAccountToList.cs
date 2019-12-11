using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Добавление организации в маркетинговый список.
    /// </summary>
    public class AddAccountToList : AddEntityToList
    {
        /// <summary>
        /// Организация.
        /// </summary>
        [RequiredArgument]
        [Input("Account")]
        [ReferenceTarget("account")]
        public InArgument<EntityReference> AccountRef { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            AddToList(context, AccountRef.Get(context).Id);
        }
    }
}