using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Добавление контакта в маркетинговый список.
    /// </summary>
    public class AddContactToList : AddEntityToList
    {
        /// <summary>
        /// Контакт.
        /// </summary>
        [RequiredArgument]
        [Input("Contact")]
        [ReferenceTarget("contact")]
        public InArgument<EntityReference> ContactRef { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            AddToList(context, ContactRef.Get(context).Id);
        }
    }
}