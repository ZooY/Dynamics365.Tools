using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Базовый класс компонентов сравнения строк.
    /// </summary>
    public abstract class ComparisonsBase : WorkflowBase
    {
        /// <summary>
        /// Первая строка.
        /// </summary>
        [RequiredArgument]
        [Input("Первая строка")]
        public InArgument<string> String1 { get; set; }


        /// <summary>
        /// Вторая строка.
        /// </summary>
        [RequiredArgument]
        [Input("Вторая строка")]
        public InArgument<string> String2 { get; set; }


        /// <summary>
        /// Результат.
        /// </summary>
        [Output("Результат")]
        public OutArgument<int> Result { get; set; }
    }
}