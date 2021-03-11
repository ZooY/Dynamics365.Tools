using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.MathTools.Workflow
{
    /// <summary>
    /// Получение произведения двух чисел.
    /// </summary>
    public class Multiplication : WorkflowBase
    {
        /// <summary>
        /// Множимое.
        /// </summary>
        [Input("Multiplicand")]
        [RequiredArgument]
        public InArgument<decimal> MultiplicandValue { get; set; }


        /// <summary>
        /// Множитель.
        /// </summary>
        [Input("Multiplier")]
        [RequiredArgument]
        public InArgument<decimal> MultiplierValue { get; set; }


        /// <summary>
        /// Произведение.
        /// </summary>
        [Output("Product")]
        public OutArgument<decimal> ProductValue { get; set; }


        protected override void Execute(Context context)
        {
            var multiplicandValue = MultiplicandValue.Get(context);
            var multiplierValue = MultiplierValue.Get(context);
            ProductValue.Set(context, multiplicandValue * multiplierValue);
        }
    }
}