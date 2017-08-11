using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Пользователь" (systemuser) для указанного GUID.
    /// </summary>
    public class GuidToUser : GuidToEntity
    {
        /// <summary>
        /// Пользователь.
        /// </summary>
        [Output("User")]
        [ReferenceTarget(Metadata.User.LogicalName)]
        public OutArgument<EntityReference> User { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, User, Metadata.User.LogicalName);
        }
    }
}