using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Создание даты из составных частей.
    /// </summary>
    public class Create : WorkflowBase
    {
        /// <summary>
        /// Год.
        /// </summary>
        [RequiredArgument]
        [Input("Year")]
        public InArgument<int> Year { get; set; }


        /// <summary>
        /// Месяц (1-12).
        /// </summary>
        [RequiredArgument]
        [Input("Month (1-12)")]
        public InArgument<int> Month { get; set; }


        /// <summary>
        /// День (1-31).
        /// </summary>
        [RequiredArgument]
        [Input("Day (1-31)")]
        public InArgument<int> Day { get; set; }


        /// <summary>
        /// Час (0-23).
        /// </summary>
        [Input("Hour (0-23)")]
        [Default("0")]
        public InArgument<int> Hour { get; set; }




        /// <summary>
        /// Минута (0-59).
        /// </summary>
        [Input("Minute (0-59)")]
        [Default("0")]
        public InArgument<int> Minute { get; set; }


        /// <summary>
        /// Секунда (0-59).
        /// </summary>
        [Input("Second (0-59)")]
        [Default("0")]
        public InArgument<int> Second { get; set; }


        /// <summary>
        /// Миллисекунда (0-999).
        /// </summary>
        [Input("Millisecond (0-999)")]
        [Default("0")]
        public InArgument<int> Millisecond { get; set; }


        /// <summary>
        /// Результирующее значение даты/времени.
        /// </summary>
        [Output("Date/Time")]
        public OutArgument<DateTime> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Result.Set(context, new DateTime(
                Year.Get(context),
                Month.Get(context),
                Day.Get(context),
                Hour.Get(context),
                Minute.Get(context),
                Second.Get(context),
                Millisecond.Get(context)
            ));
        }
    }
}