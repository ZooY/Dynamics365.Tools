using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Common.Workflow;
using PZone.Activities;

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


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var emailRef = Email.Get(context);
            var request = new SendEmailRequest { EmailId = emailRef.Id, TrackingToken = "", IssueSend = true };
            context.Service.Execute(request);
        }
    }
}