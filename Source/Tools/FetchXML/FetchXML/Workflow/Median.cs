using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;
using PZone.Xrm.Sdk;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Вычисление медианы ряда числовых значений, возвращаемых запросом FetchXML.
    /// </summary>
    /// <remarks>
    /// Для расчета используются только атрибуты с типами Decimal, Double, Int, Money.
    /// </remarks>
    public class Median : FetchXmlWorkflow
    {
        /// <summary>
        /// Результирующее числовое значение, приведенное к типу <see cref="decimal"/>.
        /// </summary>
        [Output("Result as Decimal")]
        public OutArgument<decimal> ResultAsDecimal { get; set; }


        /// <summary>
        /// Результирующее числовое значение, приведенное к типу <see cref="double"/>.
        /// </summary>
        [Output("Result as Double")]
        public OutArgument<double> ResultAsDouble { get; set; }


        /// <summary>
        /// Результирующее числовое значение в виде целого числа.
        /// </summary>
        /// <remarks>
        /// Округдение до целого числа происходит по математическим правилам.
        /// </remarks>
        [Output("Result as Integer")]
        public OutArgument<int> ResultAsInteger { get; set; }


        /// <summary>
        /// Результирующее числовое значение в виде денежного значения.
        /// </summary>
        [Output("Result as Money")]
        public OutArgument<Money> ResultAsMoney { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var query = FetchXml.Get(context);
            if (string.IsNullOrWhiteSpace(query))
                return;

            var result = context.Service.RetrieveMultiple(query);
            if (result.Entities.Count < 1)
                return;

            var values = new List<decimal>();
            foreach (var entity in result.Entities)
            {
                foreach (var attribute in entity.Attributes)
                {
                    if (attribute.Value is decimal || attribute.Value is double || attribute.Value is int)
                        values.Add(Convert.ToDecimal(attribute.Value));
                    else if (attribute.Value is Money)
                        values.Add(((Money)attribute.Value).Value);
                }
            }
            if (values.Count == 0)
                return;

            var sortedValues = values.OrderBy(number => number);
            var itemIndex = sortedValues.Count() / 2;
            var resultAsDecimal = sortedValues.Count() % 2 == 0
                ? (sortedValues.ElementAt(itemIndex) + sortedValues.ElementAt(itemIndex - 1)) / 2
                : sortedValues.ElementAt(itemIndex);

            ResultAsDecimal.Set(context, resultAsDecimal);
            ResultAsDouble.Set(context, Convert.ToDouble(resultAsDecimal));
            ResultAsInteger.Set(context, Convert.ToInt32(resultAsDecimal));
            ResultAsMoney.Set(context, new Money(resultAsDecimal));
        }
    }
}