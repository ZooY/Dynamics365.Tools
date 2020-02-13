using System;
using System.Activities;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Получение значения атрибута, возвращаемого запросом FetchXML.
    /// </summary>
    /// <remarks>
    /// <para>Для полностью корректного результата FetchXML-запрос должен 
    /// содержать только один возвращаемый атрибут.</para>
    /// <para>Если в запросе не указано ни одного атрибута, то будет возвращен
    /// идентификатор основной сущности.</para>
    /// <para>Если в запросе указано несколько атрибутов, то будет возвращено 
    /// значение первого атрибута.</para>
    /// <para>Если в запросе есть связанные сущности, то при получении значения
    /// атрибута используется только имя атрибута и не учитывается префикс. 
    /// Поэтому, если в запросе есть несколько связанных сущностей с атрибутами 
    /// с одинаковыми именами, то результат становиться непредсказуемым 
    /// (точнее, будет возвращено первое непустое значение этих 
    /// атрибутов).</para>
    /// </remarks>
    public class Value : FetchXmlWorkflow
    {
        /// <summary>
        /// Результирующее значение в виде логического значения.
        /// </summary>
        /// <remarks>
        /// Свойство имеет значение <c>false</c>, если числовое значение равно <c>0</c> и <c>true</c> в случае любых других значений.
        /// </remarks>
        [Output("Result as Boolean")]
        public OutArgument<bool> ResultAsBoolean { get; set; }


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


        /// <summary>
        /// Результирующее значение в виде строки.
        /// </summary>
        [Output("Result as String")]
        public OutArgument<string> ResultAsString { get; set; }


        /// <summary>
        /// Результирующее значение в виде даты/времени.
        /// </summary>
        [Output("Result as Date/Time")]
        public OutArgument<DateTime> ResultAsDateTime { get; set; }


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

            var entity = result.Entities.First();

            var regex = new Regex(@"<attribute name=[""'](?<name>[^\s]+)[""']\s*/>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var attributeName = regex.Matches(query).Cast<Match>().Select(match => match.Groups["name"].Value).FirstOrDefault();

            if (attributeName == null)
            {
                ResultAsString.Set(context, entity.Id.ToString());
                return;
            }

            if (entity.Attributes.Keys.All(a => a != attributeName))
                attributeName = entity.Attributes.Keys.FirstOrDefault(a => a.EndsWith($".{attributeName}"));

            if (!entity.Contains(attributeName))
                return;

            var attributeValue = entity.Attributes[attributeName];
            if (attributeValue is AliasedValue)
                attributeValue = ((AliasedValue)attributeValue).Value;

            if (attributeValue is Money)
                attributeValue = ((Money)attributeValue).Value;

            if (attributeValue is EntityReference)
                attributeValue = ((EntityReference)attributeValue).Id;

            ResultAsString.Set(context, attributeValue.ToString());

            if (attributeValue is DateTime)
            {
                ResultAsDateTime.Set(context, (DateTime)attributeValue);
                return;
            }

            if (attributeValue is OptionSetValue)
            {
                attributeValue = ((OptionSetValue)attributeValue).Value;
                // TODO: Получение метки элемента списка и отображение его в качестве текстового значения.
                ResultAsString.Set(context, attributeValue.ToString());
            }

            if (attributeValue is bool || attributeValue is decimal || attributeValue is double || attributeValue is int)
            {
                var decimalValue = Convert.ToDecimal(attributeValue);
                ResultAsBoolean.Set(context, Convert.ToBoolean(attributeValue));
                ResultAsDecimal.Set(context, decimalValue);
                ResultAsDouble.Set(context, Convert.ToDouble(attributeValue));
                ResultAsInteger.Set(context, Convert.ToInt32(attributeValue));
                ResultAsMoney.Set(context, new Money(decimalValue));
            }
        }
    }
}