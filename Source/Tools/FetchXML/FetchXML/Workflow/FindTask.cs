using System.Activities;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;

namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Поиск Задачи с помощью FetchXML-запроса.
    /// </summary>
    /// <remarks>
    /// <para>Для ускорения работы запроса следует использовать атрибут top="1".</para>
    /// <para>Если запрос возвращает несколько записей - берется первая.</para>
    /// </remarks>
    public class FindTask : FetchXmlWorkflow
    {
        /// <summary>
        /// Задача.
        /// </summary>
        [Output("Task")]
        [ReferenceTarget("task")]
        public OutArgument<EntityReference> Task { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var query = FetchXml.Get(context);
            if (string.IsNullOrWhiteSpace(query))
                return;
                        
            var service = context.SourceActivityContext.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(null);

            var task = service.RetrieveMultiple(query).Entities.FirstOrDefault();
            if (task == null)
            {
                Task.Set(context, null);
                return;
            }

            if (task.LogicalName != "task")
                throw new InvalidWorkflowExecutionException($"Найденная сущность не является Задачей (task).");

            Task.Set(context, task.ToEntityReference());
        }
    }
}