using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.MathTools.Workflow
{
    /// <summary>
    /// Получение суммы двух чисел.
    /// </summary>
    public class Sum : WorkflowBase
    {
        /// <summary>
        /// Первое слагаемое.
        /// </summary>
        [Input("Addend")]
        [RequiredArgument]
        public InArgument<decimal> AddendValue { get; set; }


        /// <summary>
        /// Второе слагаемое.
        /// </summary>
        [Input("Augend")]
        [RequiredArgument]
        public InArgument<decimal> AugendValue { get; set; }


        [Output("Sum")]
        public OutArgument<decimal> SumValue { get; set; }


        protected override void Execute(Context context)
        {
            var addendValue = AddendValue.Get(context);
            var augendValue = AugendValue.Get(context);
            SumValue.Set(context, addendValue + augendValue);
        }
    }
}