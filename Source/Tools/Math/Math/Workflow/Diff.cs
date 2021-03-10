using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.MathTools.Workflow
{
    /// <summary>
    /// Получение разницы между двумя числами в числовом и процентном выражении.
    /// </summary>
    public class Diff : WorkflowBase
    {
        /// <summary>
        /// Уменьшаемое.
        /// </summary>
        [Input("Minuend")]
        [RequiredArgument]
        public InArgument<decimal> MinuendValue { get; set; }
        
        
        /// <summary>
        /// Вычитаемое.
        /// </summary>
        [Input("Subtrahend")]
        [RequiredArgument]
        public InArgument<decimal> SubtrahendValue { get; set; }


        [Output("Numeric Difference")]
        public OutArgument<decimal> NumericDifferenceValue { get; set; }


        [Output("Percentage Difference")]
        public OutArgument<decimal> PercentageDifferenceValue { get; set; }


        protected override void Execute(Context context)
        {
            var minuendValue = MinuendValue.Get(context);
            var subtrahendValue = SubtrahendValue.Get(context);
            NumericDifferenceValue.Set(context, minuendValue - subtrahendValue);
            PercentageDifferenceValue.Set(context, Math.Abs(100 - subtrahendValue * 100 / minuendValue));
        }
    }
}