using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Получение значения переменной.
    /// </summary>
    public class GetValue : VariableBase
    {
        [Output("Has Value")]
        public OutArgument<bool> HasValue { get; set; }


        /// <summary>
        /// Значение переменной в виде строки.
        /// </summary>
        [Output("String Value")]
        public OutArgument<string> StringValue { get; set; }


        /// <summary>
        /// Значение переменной в виде булевого значения.
        /// </summary>
        [Output("Boolean Value")]
        public OutArgument<bool> BooleanValue { get; set; }


        [Output("Date/Time Value")]
        public OutArgument<DateTime> DateTimeValue { get; set; }


        [Output("Decimal Value")]
        public OutArgument<decimal> DecimalValue { get; set; }


        [Output("Double Value")]
        public OutArgument<double> DoubleValue { get; set; }


        [Output("Integer Value")]
        public OutArgument<int> IntegerValue { get; set; }


        [Output("Money Value")]
        public OutArgument<Money> MoneyValue { get; set; }


        protected override void Execute(Context context)
        {
            HasValue.Set(context, false);
            StringValue.Set(context, null);
            BooleanValue.Set(context, default(bool));
            DateTimeValue.Set(context, default(DateTime));
            DecimalValue.Set(context, default(decimal));
            MoneyValue.Set(context, default(Money));
            DoubleValue.Set(context, default(double));
            IntegerValue.Set(context, default(int));

            var key = GetKey(context);
            if (!context.SourceContext.SharedVariables.ContainsKey(key))
                return;
            var value = context.SourceContext.SharedVariables[key];
            if (value == null)
                return;

            HasValue.Set(context, true);

            var stringValue = value.ToString().Trim();
            StringValue.Set(context, stringValue);

            var booleanValue = !(stringValue.Equals("false", StringComparison.OrdinalIgnoreCase) || stringValue.Equals("0") || stringValue.Equals("+0") || stringValue.Equals("-0"));
            BooleanValue.Set(context, booleanValue);

            DateTime dateTimeValue;
            if (DateTime.TryParse(stringValue, out dateTimeValue))
                DateTimeValue.Set(context, dateTimeValue);

            decimal decimalValue;
            if (decimal.TryParse(stringValue, out decimalValue))
            {
                DecimalValue.Set(context, decimalValue);
                MoneyValue.Set(context, new Money(decimalValue));
            }

            double doubleValue;
            if (double.TryParse(stringValue, out doubleValue))
                DoubleValue.Set(context, doubleValue);

            int integerValue;
            if (int.TryParse(stringValue, out integerValue))
                IntegerValue.Set(context, integerValue);
        }
    }
}