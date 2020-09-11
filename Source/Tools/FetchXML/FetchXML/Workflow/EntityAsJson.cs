using System.Activities;
using System.Linq;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Получение сущности в виде строки в формате JSON.
    /// </summary>
    /// <remarks>
    /// <para>Если запрос возвращает несколько записей, будет возвращена первая.</para>
    /// </remarks>
    public class EntityAsJson : FetchXmlWorkflow
    {
        /// <summary>
        /// Результирующее значение в виде логического значения.
        /// </summary>
        /// <remarks>
        /// Свойство имеет значение <c>false</c>, если числовое значение равно <c>0</c> и <c>true</c> в случае любых других значений.
        /// </remarks>
        [Output("JSON")]
        public OutArgument<string> Json { get; set; }

        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            Json.Set(context, null);

            var query = FetchXml.Get(context);

            var entity = context.SystemService.RetrieveMultiple(query).Entities.FirstOrDefault();
            if (entity == null)
                return;

            Json.Set(context, JsonConvert.SerializeObject(new
            {
                entity.LogicalName,
                entity.Id,
                Attributes = entity.Attributes.ToDictionary(e=>e.Key, e=>e.Value),
                FormattedValues = entity.FormattedValues.ToDictionary(e => e.Key, e => e.Value)
            }));
        }
    }
}