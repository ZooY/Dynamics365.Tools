using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Назначение группы для всех записей, которые возвращает запрос FetchXML.
    /// </summary>
    public class AssignTeam : AssignOwner
    {
        /// <summary>
        /// Группа.
        /// </summary>
        [RequiredArgument]
        [Input("Team")]
        [ReferenceTarget("team")]
        public InArgument<EntityReference> TeamRef { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Execute(context, TeamRef.Get(context));
        }
    }
}