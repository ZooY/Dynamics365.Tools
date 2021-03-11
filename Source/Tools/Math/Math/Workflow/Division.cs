using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.MathTools.Workflow
{
    /// <summary>
    /// Получение частного двух чисел.
    /// </summary>
    public class Division : WorkflowBase
    {
        /// <summary>
        /// Делимое.
        /// </summary>
        [Input("Dividend")]
        [RequiredArgument]
        public InArgument<decimal> DividendValue { get; set; }


        /// <summary>
        /// Делитель.
        /// </summary>
        [Input("Divisor")]
        [RequiredArgument]
        public InArgument<decimal> DivisorValue { get; set; }


        /// <summary>
        /// Частное.
        /// </summary>
        [Output("Quotient ")]
        public OutArgument<decimal> QuotientValue { get; set; }


        protected override void Execute(Context context)
        {
            var dividendValue = DividendValue.Get(context);
            var divisorValue = DivisorValue.Get(context);
            QuotientValue.Set(context, dividendValue / divisorValue);
        }
    }
}