using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.DateTools.Workflow
{
    /// <summary>
    /// Расчет возраста (количества полных лет).
    /// </summary>
    public class Age : WorkflowBase
    {
        /// <summary>
        /// Исходная дата.
        /// </summary>
        [RequiredArgument]
        [Input("Date")]
        public InArgument<DateTime> Date { get; set; }


        /// <summary>
        /// Возраст (количество полных лет).
        /// </summary>
        [Output("Возраст в годах")]
        public OutArgument<int> AgeInYears { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var nowDate = DateTime.Today;
            var birthDate = Date.Get(context);
            var age = nowDate.Year - birthDate.Year;
            if (birthDate > nowDate.AddYears(-age))
                age--;
            AgeInYears.Set(context, age);
        }
    }
}