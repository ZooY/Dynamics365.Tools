using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Получение подстроки.
    /// </summary>
    public class Substring : WorkflowBase
    {
        /// <summary>
        /// Входная строка.
        /// </summary>
        [RequiredArgument]
        [Input("Входная строка")]
        [Output("Выходная строка")]
        public InOutArgument<string> String { get; set; }

        /// <summary>
        /// Начальный индекс.
        /// </summary>
        [RequiredArgument]
        [Input("Начальный индекс")]
        [Default("0")]
        public InArgument<int> StartIndex { get; set; }

        /// <summary>
        /// Длина подстроки
        /// </summary>
        [Input("Длина подстроки")]
        public InArgument<int> Length { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var str = String.Get(context);
            var start = StartIndex.Get(context);
            var len = Length.Get(context);
            if (len == 0)
                len = str.Length - start;
            String.Set(context, str.Substring(start, len));
        }
    }
}