using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Текущая дата.
    /// </summary>
    public class Now : WorkflowBase
    {
        /// <summary>
        /// Полное значение текущей локальной даты/времени.
        /// </summary>
        [Output("Date/Time Local")]
        public OutArgument<DateTime> DateTimeLocal { get; set; }


        /// <summary>
        /// Полное значение текущей даты/времени в UTC.
        /// </summary>
        [Output("Date/Time UTC")]
        public OutArgument<DateTime> DateTimeUtc { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            DateTimeLocal.Set(context, now);
            DateTimeUtc.Set(context, utcNow);
        }
    }
}