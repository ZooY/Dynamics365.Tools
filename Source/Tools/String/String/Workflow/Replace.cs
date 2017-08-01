using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Замена подстроки в строке.
    /// </summary>
    public class Replace : WorkflowBase
    {
        /// <summary>
        /// Входная строка.
        /// </summary>
        [RequiredArgument]
        [Input("Входная строка")]
        [Output("Выходная строка")]
        public InOutArgument<string> String { get; set; }


        /// <summary>
        /// Строка для поиска.
        /// </summary>
        [RequiredArgument]
        [Input("Строка для поиска")]
        public InArgument<string> OldValue { get; set; }


        /// <summary>
        /// Строка замены.
        /// </summary>
        [Input("Строка замены")]
        public InArgument<string> NewValue { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var str = String.Get(context);
            var oldValue = OldValue.Get(context);
            if (str == null || oldValue == null)
            {
                String.Set(context, null);
                return;
            }
            var newValue = NewValue.Get(context) ?? "";
            String.Set(context, str.Replace(oldValue, newValue));
        }
    }
}
