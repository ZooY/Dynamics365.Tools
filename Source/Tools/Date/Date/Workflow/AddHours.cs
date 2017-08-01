using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Добавлеине к дате указаное количество чвсов.
    /// </summary>
    public class AddHours : WorkflowBase
    {
        /// <summary>
        /// Исходная дата.
        /// </summary>
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> Date { get; set; }


        /// <summary>
        /// Количество часов.
        /// </summary>
        [RequiredArgument]
        [Input("Hours count")]
        public InArgument<int> Count { get; set; }


        /// <summary>
        /// Результирующее значение даты/времени.
        /// </summary>
        [Output("Date/Time")]
        public OutArgument<DateTime> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Result.Set(context, Date.Get(context).AddHours(Count.Get(context)));
        }
    }
}