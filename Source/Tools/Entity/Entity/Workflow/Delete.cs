using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Удаление произвольной записи.
    /// </summary>
    public class Delete : WorkflowBase
    {
        /// <summary>
        /// Тип удаляемой записи.
        /// </summary>
        [RequiredArgument]
        [Input("Entity Logical Name")]
        public InArgument<string> EntityName { get; set; }


        /// <summary>
        /// Идентификатор удаляемой сущности.
        /// </summary>
        [RequiredArgument]
        [Input("Entity GUID")]
        public InArgument<string> EntityId { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            context.Service.Delete(EntityName.Get(context), Guid.Parse(EntityId.Get(context)));
        }
    }
}