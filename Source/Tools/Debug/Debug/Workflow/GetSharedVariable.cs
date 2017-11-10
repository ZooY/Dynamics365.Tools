using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;


namespace PZone.DebugTools.Workflow
{
    /// <summary>
    /// Получение значения общей переменной.
    /// </summary>
    public class GetSharedVariable : WorkflowBase
    {
        /// <summary>
        /// Имя переменной.
        /// </summary>
        [Input("Variable Name")]
        [RequiredArgument]
        public InArgument<string> VariableName { get; set; }


        /// <summary>
        /// Тип значения переменной.
        /// </summary>
        [Output("Value Type")]
        public OutArgument<string> ValueType { get; set; }


        /// <summary>
        /// Флаг наличия значения переменной.
        /// </summary>
        [Output("Has Value")]
        public OutArgument<bool> HasValue { get; set; }


        /// <summary>
        /// Значение переменной в виде строки.
        /// </summary>
        [Output("String Value")]
        public OutArgument<string> StringValue { get; set; }


        /// <summary>
        /// Значение переменной в виде строки в формате JSON.
        /// </summary>
        [Output("JSON String Value")]
        public OutArgument<string> JsonValue { get; set; }


        /// <summary>
        /// Значение переменной в виде булевого значения.
        /// </summary>
        [Output("Boolean Value")]
        public OutArgument<bool> BooleanValue { get; set; }

        /// <summary>
        /// Значение переменной в формате даты/времени.
        /// </summary>
        [Output("Date/Time Value")]
        public OutArgument<DateTime> DateTimeValue { get; set; }

        /// <summary>
        /// Значение переменной типа <see cref="decimal"/>.
        /// </summary>
        [Output("Decimal Value")]
        public OutArgument<decimal> DecimalValue { get; set; }

        /// <summary>
        /// Значение переменной типа <see cref="double"/>.
        /// </summary>
        [Output("Double Value")]
        public OutArgument<double> DoubleValue { get; set; }

        /// <summary>
        /// Значение переменной типа <see cref="int"/>.
        /// </summary>
        [Output("Integer Value")]
        public OutArgument<int> IntegerValue { get; set; }

        /// <summary>
        /// Значение переменной типа <see cref="Money"/>.
        /// </summary>
        [Output("Money Value")]
        public OutArgument<Money> MoneyValue { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            HasValue.Set(context, false);
            ValueType.Set(context, null);
            StringValue.Set(context, null);
            JsonValue.Set(context, null);
            BooleanValue.Set(context, default(bool));
            DateTimeValue.Set(context, default(DateTime));
            DecimalValue.Set(context, default(decimal));
            MoneyValue.Set(context, default(Money));
            DoubleValue.Set(context, default(double));
            IntegerValue.Set(context, default(int));

            var variableName = VariableName.Get(context);
            if (string.IsNullOrWhiteSpace(variableName))
                throw new InvalidWorkflowExecutionException("Variable Name is empty.");

            if (!context.SourceContext.SharedVariables.ContainsKey(variableName))
                return;
            var value = context.SourceContext.SharedVariables[variableName];
            if (value == null)
            {
                ValueType.Set(context, "Null");
                return;
            }

            ValueType.Set(context, value.GetType().Name);

            HasValue.Set(context, true);

            var stringValue = value as string ?? value.ToString();
            StringValue.Set(context, stringValue);

            var booleanValue = value as bool? ?? !(stringValue.Equals("false", StringComparison.OrdinalIgnoreCase) || stringValue.Equals("0") || stringValue.Equals("+0") || stringValue.Equals("-0"));
            BooleanValue.Set(context, booleanValue);

            DateTime dateTimeValue;
            if (value is DateTime)
                DateTimeValue.Set(context, (DateTime)value);
            else if (DateTime.TryParse(stringValue, out dateTimeValue))
                DateTimeValue.Set(context, dateTimeValue);

            decimal decimalValue;
            if (value is decimal)
            {
                decimalValue = (decimal)value;
                DecimalValue.Set(context, decimalValue);
                MoneyValue.Set(context, new Money(decimalValue));
            }
            else if (decimal.TryParse(stringValue, out decimalValue))
            {
                DecimalValue.Set(context, decimalValue);
                MoneyValue.Set(context, new Money(decimalValue));
            }

            var moneyValue = value as Money;
            if (moneyValue != null)
            {
                DecimalValue.Set(context, moneyValue.Value);
                MoneyValue.Set(context, moneyValue);
            }

            double doubleValue;
            if (value is double)
                DoubleValue.Set(context, (double)value);
            else if (double.TryParse(stringValue, out doubleValue))
                DoubleValue.Set(context, doubleValue);

            int integerValue;
            if (value is int)
                IntegerValue.Set(context, (int)value);
            else if (int.TryParse(stringValue, out integerValue))
                IntegerValue.Set(context, integerValue);

            JsonValue.Set(context, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            }));
        }
    }
}