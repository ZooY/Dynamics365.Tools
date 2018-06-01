using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Получение отдельных составных частей даты.
    /// </summary>
    public class Parts : WorkflowBase
    {
        /// <summary>
        /// Входное значение даты.
        /// </summary>
        [RequiredArgument]
        [Input("Input Date")]
        public InArgument<DateTime> InputDateTime { get; set; }


        /// <summary>
        /// Значение даты без времени (с нулевым значением времени) в формате <see cref="DateTime"/>.
        /// </summary>
        [Output("Date")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<DateTime> DateAsDT { get; set; }


        /// <summary>
        /// Значение времени без даты (с датой 01.01.0001) в формате <see cref="DateTime"/>.
        /// </summary>
        [Output("Time")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<DateTime> TimeAsDT { get; set; }


        /// <summary>
        /// Значение дня.
        /// </summary>
        [Output("Day")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Day { get; set; }


        /// <summary>
        /// Значение месяца.
        /// </summary>
        [Output("Month")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Month { get; set; }


        /// <summary>
        /// Значение года.
        /// </summary>
        [Output("Year")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Year { get; set; }


        /// <summary>
        /// Значение года в виде строки.
        /// </summary>
        /// <remarks>
        /// При использовании в качестве строки целочисленного значения из свойства <see cref="Year"/>, оно возвращается в форматированном виде с пробелом в качестве разделителя разрядов. 
        /// Поэтому для получения года в "нормальном" виде необходимо использовать его строковое представление.
        /// </remarks>
        [Output("Year as String")]
        public OutArgument<string> YearAsString { get; set; }


        /// <summary>
        /// Значение часа.
        /// </summary>
        [Output("Hour")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Hour { get; set; }


        /// <summary>
        /// Значение минут.
        /// </summary>
        [Output("Minutes")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Minute { get; set; }


        /// <summary>
        /// Значение секунд.
        /// </summary>
        [Output("Seconds")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<int> Second { get; set; }


        /// <summary>
        /// Значение типа даты.
        /// </summary>
        [Output("Type")]
        // ReSharper disable once InconsistentNaming
        public OutArgument<string> Kind { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var dateTime = InputDateTime.Get(context);
            DateAsDT.Set(context, new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0));
            TimeAsDT.Set(context, new DateTime(1, 1, 1, dateTime.Hour, dateTime.Minute, dateTime.Second));
            Day.Set(context, dateTime.Day);
            Month.Set(context, dateTime.Month);
            Year.Set(context, dateTime.Year);
            YearAsString.Set(context, dateTime.Year.ToString());
            Hour.Set(context, dateTime.Hour);
            Minute.Set(context, dateTime.Minute);
            Second.Set(context, dateTime.Second);
            Kind.Set(context, dateTime.Kind.ToString());
        }
    }
}
