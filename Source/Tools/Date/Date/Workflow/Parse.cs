using System;
using System.Activities;
using System.Globalization;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Преобразование строки в дату.
    /// </summary>
    public class Parse : WorkflowBase
    {
        /// <summary>
        /// дата в виде строки.
        /// </summary>
        [RequiredArgument]
        [Input("Date string")]
        public InArgument<string> DateString { get; set; }


        /// <summary>
        /// Формат даты.
        /// </summary>
        [Input("Date format")]
        public InArgument<string> DateFormat { get; set; }


        /// <summary>
        /// Результирующее значение даты/времени.
        /// </summary>
        [Output("Date/Time")]
        public OutArgument<DateTime> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var dateString = DateString.Get(context);
            var format = DateFormat.Get(context);
            var date = string.IsNullOrWhiteSpace(format)
                ? DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault)
                : DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);
            Result.Set(context, date);
        }
    }
}