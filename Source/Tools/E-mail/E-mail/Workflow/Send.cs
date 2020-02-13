using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

using PZone.Xrm.Workflow;


namespace PZone.EmailTools.Workflow
{
    /// <summary>
    /// Отправка существующего электронного письма.
    /// </summary>
    public class Send : WorkflowBase
    {
        /// <summary>
        /// Ссылка на сущность электронной почты.
        /// </summary>
        [Input("E-mail")]
        [ReferenceTarget("email")]
        public InArgument<EntityReference> Email { get; set; }


        /// <summary>
        /// Выполнение запросов в CRM от имени системного пользователя.
        /// </summary>
        [Input("Execute as SYSTEM User")]
        public InArgument<bool> ExecureAsSystem { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var emailRef = Email.Get(context);
            var request = new SendEmailRequest { EmailId = emailRef.Id, TrackingToken = "", IssueSend = true };
            var service = ExecureAsSystem.Get(context) ? context.SystemService : context.Service;
            service.Execute(request);
        }
    }
}