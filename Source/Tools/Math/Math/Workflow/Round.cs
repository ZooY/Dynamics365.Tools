using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.MathTools.Workflow
{
    /// <summary>
    /// Округление числа по математическим правилам, а также в большую и меньшу стороны.
    /// </summary>
    public class Round : WorkflowBase
    {
        [Input("Value")]
        [RequiredArgument]
        public InArgument<decimal> Value { get; set; }
        
        
        [Input("Digits")]
        [RequiredArgument]
        public InArgument<int> Digits { get; set; }


        [Output("Round")]
        public OutArgument<decimal> RoundValue { get; set; }


        [Output("Ceiling")]
        public OutArgument<decimal> CeilingValue { get; set; }


        [Output("Floor")]
        public OutArgument<decimal> FloorValue { get; set; }


        protected override void Execute(Context context)
        {
            var value = Value.Get(context);
            var digits = Digits.Get(context);

            if (digits < 0)
                throw new InvalidWorkflowException("Digits value cannot be negative.");
            
            var m = digits == 0 ? 1 : (decimal)Math.Pow(10, digits);
            var tmp = value * m;
            var multiplier = value > 0 ? 1 : -1;

            if (tmp % 1 == 0)
            {
                RoundValue.Set(context, value);
                CeilingValue.Set(context, value);
                FloorValue.Set(context, value);
                return;
            }

            RoundValue.Set(context, Math.Abs(Math.Round(value, digits, MidpointRounding.AwayFromZero)) * multiplier);
            CeilingValue.Set(context, Math.Ceiling(tmp) / m);
            FloorValue.Set(context, Math.Floor(tmp) / m);
        }
    }
}