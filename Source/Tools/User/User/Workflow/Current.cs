using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.UserTools.Workflow
{
    //public class BusinessUnit : WorkflowBase
    //{
    //    /// <summary>
    //    /// Пользователь, для которого нужно получить информацию о подразделении.
    //    /// </summary>
    //    [Input("User")]
    //    [ReferenceTarget("systemuser")]
    //    public InArgument<EntityReference> User { get; set; }


    //    /// <summary>
    //    /// Подразделение пользователя.
    //    /// </summary>
    //    [Output("Business Unit")]
    //    [ReferenceTarget("businessunit")]
    //    public OutArgument<EntityReference> BusinessUnit { get; set; }


    //    /// <summary>
    //    /// Пользователь, инициировавший запуск процесса.
    //    /// </summary>
    //    [Output("Initiating User")]
    //    [ReferenceTarget("systemuser")]
    //    public OutArgument<EntityReference> InitiatingUser { get; set; }


    //    /// <inheritdoc />
    //    protected override void Execute(Context context)
    //    {
    //        CurrentUser.Set(context, new EntityReference("systemuser", context.SourceContext.UserId));
    //        InitiatingUser.Set(context, new EntityReference("systemuser", context.SourceContext.InitiatingUserId));
    //    }
    //}

    /// <summary>
    /// Получение текущих пользователей, кто инициировал запуск процесса и от чьего имени выполняется процесс.
    /// </summary>
    public class Current : WorkflowBase
    {
        /// <summary>
        /// Пользователь, от имени которого выполняется процесс.
        /// </summary>
        [Output("Current User")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> CurrentUser { get; set; }


        /// <summary>
        /// Пользователь, инициировавший запуск процесса.
        /// </summary>
        [Output("Initiating User")]
        [ReferenceTarget("systemuser")]
        public OutArgument<EntityReference> InitiatingUser { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            CurrentUser.Set(context, new EntityReference("systemuser", context.SourceContext.UserId));
            InitiatingUser.Set(context, new EntityReference("systemuser", context.SourceContext.InitiatingUserId));
        }
    }
}