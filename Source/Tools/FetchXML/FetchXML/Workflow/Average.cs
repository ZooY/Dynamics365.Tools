using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Вычисление среднего арифметического числовых значений, возвращаемых запросом FetchXML.
    /// </summary>
    /// <remarks>
    /// Для расчета используются только атрибуты с типами Decimal, Double, Int, Money.
    /// </remarks>
    public class Average : FetchXmlWorkflow
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

            var service = ExecureAsSystem.Get(context) ? context.SystemService : context.Service;

            var result = service.RetrieveMultiple(query);
            if (result.Entities.Count < 1)
                return;

            decimal resultAsDecimal = 0;
            var valuesCount = 0;
            foreach (var entity in result.Entities)
            {
                foreach (var attribute in entity.Attributes)
                {
                    if (attribute.Value is decimal || attribute.Value is double || attribute.Value is int)
                    {
                        resultAsDecimal += Convert.ToDecimal(attribute.Value);
                        valuesCount++;
                    }
                    else if (attribute.Value is Money)
                    {
                        resultAsDecimal += ((Money)attribute.Value).Value;
                        valuesCount++;
                    }
                }
            }
            resultAsDecimal = resultAsDecimal / valuesCount;
            ResultAsDecimal.Set(context, resultAsDecimal);
            ResultAsDouble.Set(context, Convert.ToDouble(resultAsDecimal));
            ResultAsInteger.Set(context, Convert.ToInt32(resultAsDecimal));
            ResultAsMoney.Set(context, new Money(resultAsDecimal));
        }
    }
}