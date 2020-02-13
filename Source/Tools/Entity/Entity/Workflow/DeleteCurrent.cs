using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Удаление текущей записи.
    /// </summary>
    public class DeleteCurrent : WorkflowBase
    {
        /// <summary>
        /// Выполнение запросов в CRM от имени системного пользователя.
        /// </summary>
        [Input("Execute as SYSTEM User")]
        public InArgument<bool> ExecureAsSystem { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            if (string.IsNullOrEmpty(context.EntityName) || context.EntityId == Guid.Empty)
                return;
            var service = ExecureAsSystem.Get(context) ? context.SystemService : context.Service;
            service.Delete(context.EntityName, context.EntityId);
        }
    }
}