using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Common.Workflow;
using PZone.Activities;
using PZone.Xrm.Sdk;

namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Получение количества записей, возвращаемых запросом FetchXML.
    /// </summary>
    public class Count : FetchXmlWorkflow
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


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var query = FetchXml.Get(context);
            if (string.IsNullOrWhiteSpace(query))
                return;

            var result = context.Service.RetrieveMultiple(query);
            ResultAsDecimal.Set(context, result.Entities.Count);
            ResultAsDouble.Set(context, Convert.ToDouble(result.Entities.Count));
            ResultAsInteger.Set(context, Convert.ToInt32(result.Entities.Count));
        }
    }
}