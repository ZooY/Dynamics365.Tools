using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Перевод всех букв строки в нижний регистр.
    /// </summary>
    public class ToLowerCase : WorkflowBase
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
            String.Set(context, String.Get(context).ToLower());
        }
    }
}