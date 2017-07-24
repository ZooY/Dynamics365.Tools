using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Назначение пользователя для всех записей, которые возвращает запрос FetchXML.
    /// </summary>
    public class AssignUser : AssignOwner
    {
        /// <summary>
        /// Системный пользователь.
        /// </summary>
        [RequiredArgument]
        [Input("User")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> UserRef { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Execute(context, UserRef.Get(context));
        }
    }
}