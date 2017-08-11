using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Добавлеине к дате указаное количество миллисекунд.
    /// </summary>
    public class AddMilliseconds : WorkflowBase
    {
        /// <summary>
        /// Исходная дата.
        /// </summary>
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> Date { get; set; }


        /// <summary>
        /// Количество миллисекунд.
        /// </summary>
        [RequiredArgument]
        [Input("Milliseconds count")]
        public InArgument<int> Count { get; set; }


        /// <summary>
        /// Результирующее значение даты/времени.
        /// </summary>
        [Output("Date/Time")]
        public OutArgument<DateTime> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Result.Set(context, Date.Get(context).AddMilliseconds(Count.Get(context)));
        }
    }
}