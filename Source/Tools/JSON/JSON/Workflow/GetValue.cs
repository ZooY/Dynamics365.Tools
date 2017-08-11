using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;


namespace PZone.JsonTools.Workflow
{
    /// <summary>
    /// Получение значения из строки формата JSON.
    /// </summary>
    public class GetValue : WorkflowBase
    {
        [Input("JSON")]
        [RequiredArgument]
        public InArgument<string> Json { get; set; }


        [Input("Value Path")]
        [RequiredArgument]
        public InArgument<string> ValuePath { get; set; }


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

            var json = Json.Get(context);
            Dictionary<string, object> node;
            try
            {
                node = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidWorkflowExecutionException($"Не удалось произвести разбор строки JSON. {ex.Message}", ex);
            }

            var names = ValuePath.Get(context).Split('.').ToArray();

            foreach (var name in names.Take(names.Length - 1))
            {
                if (!node.ContainsKey(name))
                    return;
                var jObj = node[name] as JObject;
                if (jObj == null)
                    return;
                node =  jObj.ToObject<Dictionary<string, object>>();
                if (node == null)
                    return;
            }

            var lastName = names.Last();
            if (!node.ContainsKey(lastName))
                return;

            var value = node[lastName];
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