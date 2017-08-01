//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;
//using Npf.Common.Workflow;


//namespace PZone.FetchXmlTools.Workflow
//{
//    /// <summary>
//    /// Получение сконкатинрованных значений запроса
//    /// </summary>
//    public class ConcatValues : WorkflowBase
//    {
//        /// <inheritdoc />
//        protected override string LoggerName => "FetchXML Tools";

//        /// <summary>
//        /// Запрос FetchXML.
//        /// </summary>
//        [RequiredArgument]
//        [Input("FetchXML string")]
//        public InArgument<string> FetchXmlString { get; set; }

//        /// <summary>
//        /// Маска вывода результата
//        /// </summary>
//        [Input("Маска вывода результата")]
//        public InArgument<string> ResultMask { get; set; }

//        /// <summary>
//        /// Результирующее значение в виде строки.
//        /// </summary>
//        [Output("Result as String")]
//        public OutArgument<string> ResultAsString { get; set; }

//        /// <inheritdoc />
//        protected override void Execute(Context context)
//        {
//            var query = FetchXmlString.Get(context);
//            if (string.IsNullOrWhiteSpace(query))
//                return;

//            var result = context.Service.RetrieveMultiple(new FetchExpression(query));
//            if (result.Entities.Count < 1)
//                return;

//            var entity = result.Entities.First();
//            var mask = ResultMask.Get(context);
//            ResultAsString.Set(context, string.IsNullOrWhiteSpace(mask) ? string.Join(", ", GetVal(entity.Attributes)) : string.Format(mask, GetVal(entity.Attributes)));
//        }


//        private object[] GetVal(AttributeCollection fetchResult)
//        {
//            var result = new List<object>();
//            foreach (var attribute in fetchResult)
//            {
//                var attributeValue = attribute.Value;

//                if (attributeValue is AliasedValue)
//                    result.Add(((AliasedValue)attributeValue).Value);

//                else if (attributeValue is bool)
//                    result.Add(Convert.ToString(attributeValue));
//                else if (attributeValue is DateTime)
//                {
//                    var datetimeValue = (DateTime)attributeValue;
//                    result.Add(Convert.ToString(datetimeValue.ToString("s")));
//                }
//                else if (attributeValue is decimal || attributeValue is double || attributeValue is int)
//                    result.Add(Convert.ToString(attributeValue));
//                else if (attributeValue is Money)
//                {
//                    var moneyValue = (Money)attributeValue;
//                    var decimalValue = moneyValue.Value;
//                    result.Add(Convert.ToString(decimalValue, CultureInfo.InvariantCulture));
//                }
//                else if (attributeValue is EntityReference)
//                    result.Add(((EntityReference)attributeValue).Id.ToString());
//                else if (attributeValue is OptionSetValue)
//                {
//                    var intValue = ((OptionSetValue)attributeValue).Value;
//                    result.Add(Convert.ToString(intValue));
//                }
//                else
//                    result.Add(attributeValue.ToString());
//            }

//            return result.ToArray();
//        }
//    }
//}