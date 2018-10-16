using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Getting the GUID of the Account entity.
    /// </summary>
    public class AccountToGuid : EntityToGuid
    {
        /// <summary>
        /// Account.
        /// </summary>
        [Input("Account")]
        [ReferenceTarget(Metadata.Account.LogicalName)]
        public InArgument<EntityReference> Account { get; set; }


        protected override void Execute(Context context)
        {
            Guid.Set(context, Account.Get(context).Id.ToString().Replace("{", "").Replace("}", ""));
        }
    }
}