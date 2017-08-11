using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Добавлеине к дате указаное количество минут.
    /// </summary>
    public class AddMinutes : WorkflowBase
    {
        /// <summary>
        /// Исходная дата.
        /// </summary>
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> Date { get; set; }


        /// <summary>
        /// Количество минут.
        /// </summary>
        [RequiredArgument]
        [Input("Minutes count")]
        public InArgument<int> Count { get; set; }


        /// <summary>
        /// Результирующее значение даты/времени.
        /// </summary>
        [Output("Date/Time")]
        public OutArgument<DateTime> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Result.Set(context, Date.Get(context).AddMinutes(Count.Get(context)));
        }
    }
}