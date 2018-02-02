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
        /// Флаг учета регистра символов при сравнении.
        /// </summary>
        [RequiredArgument]
        [Input("Учитывать регистр символов")]
        [Default("false")]
        public InArgument<bool> CaseSensitive { get; set; }
        /// <summary>
        /// Флаг учета диакритических знаков при сравнении.
        /// </summary>
        [RequiredArgument]
        [Input("Учитывать диакритические символы")]
        [Default("false")]
        public InArgument<bool> AccentSensitive { get; set; }

        /// <summary>
        /// Результат.
        /// </summary>
        [Output("Результат")]
        public OutArgument<int> Result { get; set; }
    }
}