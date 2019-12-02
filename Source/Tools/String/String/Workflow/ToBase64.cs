using System;
using System.Activities;
using System.Text;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Кодирование строки в BASE64.
    /// </summary>
    public class ToBase64 : WorkflowBase
    {
        /// <summary>
        /// Входная строка.
        /// </summary>
        [RequiredArgument]
        [Input("Входная строка")]
        [Output("Выходная строка")]
        public InOutArgument<string> String { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var str = String.Get(context);
            if (string.IsNullOrWhiteSpace(str))
                return;

            var bytes = Encoding.UTF8.GetBytes(str);
            String.Set(context, Convert.ToBase64String(bytes));
        }
    }
}