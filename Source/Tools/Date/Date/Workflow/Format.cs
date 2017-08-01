using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Форматирвоание даты и представление ее в виде строки.
    /// </summary>
    public class Format : WorkflowBase
    {
        /// <summary>
        /// Входная дата.
        /// </summary>
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> Date { get; set; }


        /// <summary>
        /// Формат даты.
        /// </summary>
        [RequiredArgument]
        [Input("Date format")]
        [Default("dd.MM.yyyy HH:mm:ss")]
        public InArgument<string> FormatString { get; set; }


        /// <summary>
        /// Форматированная дата.
        /// </summary>
        [Output("Formatted date")]
        public OutArgument<string> Result { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var date = Date.Get(context);
            var format = FormatString.Get(context);
            Result.Set(context, date.ToString(format));
        }
    }
}