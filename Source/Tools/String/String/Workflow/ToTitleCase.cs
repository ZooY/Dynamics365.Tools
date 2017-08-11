using System.Activities;
using System.Globalization;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Перевод первых букв всех слов строки в верхний регистр.
    /// </summary>
    public class ToTitleCase : WorkflowBase
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
            String.Set(context, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(String.Get(context)));
        }
    }
}