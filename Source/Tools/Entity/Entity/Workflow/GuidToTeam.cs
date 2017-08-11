using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "������ �������������" (team) ��� ���������� GUID.
    /// </summary>
    public class GuidToTeam : GuidToEntity
    {
        /// <summary>
        /// ������ �������������.
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