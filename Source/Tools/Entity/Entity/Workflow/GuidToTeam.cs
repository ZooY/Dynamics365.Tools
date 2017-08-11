using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Группа пользователей" (team) для указанного GUID.
    /// </summary>
    public class GuidToTeam : GuidToEntity
    {
        /// <summary>
        /// Группа пользователей.
        /// </summary>
        [Output("Team")]
        [ReferenceTarget(Metadata.Team.LogicalName)]
        public OutArgument<EntityReference> Team { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Team, Metadata.Team.LogicalName);
        }
    }
}