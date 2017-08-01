using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Разница между двумя датами.
    /// </summary>
    public class Diff : WorkflowBase
    {
        /// <summary>
        /// Первая дата.
        /// </summary>
        [RequiredArgument]
        [Input("First date")]
        public InArgument<DateTime> FirstDate { get; set; }


        /// <summary>
        /// Вторая дата.
        /// </summary>
        [RequiredArgument]
        [Input("Second date")]
        public InArgument<DateTime> SecondDate { get; set; }


        /// <summary>
        /// Разница в миллисекундах.
        /// </summary>
        [Output("Difference in milliseconds")]
        public OutArgument<int> DifferenceInWholeMilliseconds { get; set; }



        /// <summary>
        /// Разница в секундах.
        /// </summary>
        [Output("Difference in seconds")]
        public OutArgument<double> DifferenceInSeconds { get; set; }


        /// <summary>
        /// Разница в секундах.
        /// </summary>
        [Output("Difference in whole seconds")]
        public OutArgument<int> DifferenceInWholeSeconds { get; set; }


        /// <summary>
        /// Разница в минутах.
        /// </summary>
        [Output("Difference in minutes")]
        public OutArgument<double> DifferenceInMinutes { get; set; }


        /// <summary>
        /// Разница в целых минутах.
        /// </summary>
        [Output("Difference in whole minutes")]
        public OutArgument<int> DifferenceInWholeMinutes { get; set; }


        /// <summary>
        /// Разница в часах.
        /// </summary>
        [Output("Difference in hours")]
        public OutArgument<double> DifferenceInHours { get; set; }


        /// <summary>
        /// Разница в целых часах.
        /// </summary>
        [Output("Difference in whole hours")]
        public OutArgument<int> DifferenceInWholeHours { get; set; }


        /// <summary>
        /// Разница в днях.
        /// </summary>
        [Output("Difference in days")]
        public OutArgument<double> DifferenceInDays { get; set; }


        /// <summary>
        /// Разница в целых днях.
        /// </summary>
        [Output("Difference in whole days")]
        public OutArgument<int> DifferenceInWholeDays { get; set; }


        /// <summary>
        /// Разница в целых месяцах.
        /// </summary>
        [Output("Difference in months")]
        public OutArgument<double> DifferenceInMonths { get; set; }


        /// <summary>
        /// Разница в целых месяцах.
        /// </summary>
        [Output("Difference in whole months")]
        public OutArgument<int> DifferenceInWholeMonths { get; set; }


        /// <summary>
        /// Разница в годах.
        /// </summary>
        [Output("Difference in years")]
        public OutArgument<double> DifferenceInYears { get; set; }


        /// <summary>
        /// Разница в целых годах.
        /// </summary>
        [Output("Difference in whole years")]
        public OutArgument<int> DifferenceInWholeYears { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var firstDate = FirstDate.Get(context);
            var secondDate = SecondDate.Get(context);

            var minuend = firstDate;       // уменьшаемое
            var subtrahend = secondDate;    // вычитаемое
            if (firstDate < secondDate)
            {
                minuend = secondDate;
                subtrahend = firstDate;
            }

            var diff = minuend - subtrahend;

            DifferenceInWholeMilliseconds.Set(context, (int)Math.Floor(diff.TotalMilliseconds));

            DifferenceInSeconds.Set(context, diff.TotalSeconds);
            DifferenceInWholeSeconds.Set(context, (int)Math.Floor(diff.TotalSeconds));

            DifferenceInMinutes.Set(context, diff.TotalMinutes);
            DifferenceInWholeMinutes.Set(context, (int)Math.Floor(diff.TotalMinutes));

            DifferenceInHours.Set(context, diff.TotalHours);
            DifferenceInWholeHours.Set(context, (int)Math.Floor(diff.TotalHours));

            DifferenceInDays.Set(context, diff.TotalDays);
            DifferenceInWholeDays.Set(context, diff.Days);

            var wholeMonths = 0;
            var counter = subtrahend;
            while (minuend.CompareTo(counter) > 0)
            {
                wholeMonths++;
                counter = counter.AddMonths(1);
            }
            wholeMonths--;
            counter = counter.AddMonths(-1);
            var daysInMonth = (double)DateTime.DaysInMonth(counter.Year, counter.Month);
            var monthDiff = minuend - counter;
            var months = monthDiff.Days + (monthDiff.TotalMilliseconds / (daysInMonth * 24 * 60 * 60 * 1000));
            DifferenceInMonths.Set(context, months);
            DifferenceInWholeMonths.Set(context, wholeMonths);

            var years = (double)wholeMonths / 12;
            DifferenceInYears.Set(context, years);
            DifferenceInWholeYears.Set(context, (int)Math.Floor(years));
        }
    }
}