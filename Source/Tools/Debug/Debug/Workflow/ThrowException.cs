using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DebugTools.Workflow
{
    /// <summary>
    /// Вызов исключения.
    /// </summary>
    public class ThrowException : WorkflowBase
    {
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        [Input("Message")]
        public InArgument<string> Message { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var message = Message.Get(context);
            throw new Exception(message);
        }
    }
}