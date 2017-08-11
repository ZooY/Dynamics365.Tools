using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Перевод первой буквы строки в верхний регистр.
    /// </summary>
    public class ToFirstTitleCase : WorkflowBase
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
            String.Set(context, str.Substring(0, 1).ToUpper() + str.Substring(1));
        }
    }
}