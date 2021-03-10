using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;

namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Запуск Action по результатам выполнения FetchXML.
    /// </summary>
    public class ExecuteAction : WorkflowBase
    {
        private string _actionName = null;
        private Dictionary<string, Type> _actionProperties = null;

        private Dictionary<string, Type> _crmTypes = new Dictionary<string, Type> {
            { "EntityCollection", typeof(EntityCollection) },
            { "EntityReference", typeof(EntityReference)},
            { "OptionSetValue", typeof(OptionSetValue)},
            { "String", typeof(string)},
            { "DateTime", typeof(DateTime)},
            { "Money", typeof(Money)},
            { "Decimal", typeof(decimal)},
            { "Boolean", typeof(bool)},
            { "Double", typeof(double)},
            { "Entity", typeof(Entity)},
            { "Int32", typeof(int)}
        };


        /// <summary>
        /// Запрос FetchXML, получающий список записей.
        /// </summary>
        [RequiredArgument]
        [Input("FetchXML Query")]
        public InArgument<string> FetchXml { get; set; }


        /// <summary>
        /// Workflow
        /// </summary>
        [RequiredArgument]
        [Input("Action")]
        [ReferenceTarget("workflow")]
        public InArgument<EntityReference> Action { get; set; }


        /// <summary>
        /// JSON с параметрами.
        /// </summary>
        [Input("JSON with Parameters")]
        public InArgument<string> Parameters { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var fetchXmlStr = FetchXml.Get(context);
            var actionRef = Action.Get(context);
            var parametersJsonStr = Parameters.Get(context);

            // Получение имени и параметров действия.
            if (string.IsNullOrEmpty(_actionName))
                RetrieveActionProperties(context.SystemService, actionRef, out _actionName, out _actionProperties);

            // Формирвоание параметров действия.
            var parameters = new Dictionary<string, object>();
            foreach (var property in JObject.Parse(parametersJsonStr).Properties())
                parameters.Add(property.Name, property.Value.ToObject(_actionProperties[property.Name]));

            var moreRecords = false;
            int page = 1;
            string query = fetchXmlStr;
            do
            {
                // Получение списка сущностей.
                var collection = context.SystemService.RetrieveMultiple(new FetchExpression(query));
                if (collection.Entities.Count < 1)
                    break;

                // Формирование запросов для вызова действий.
                var requests = new ExecuteMultipleRequest()
                {
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    Requests = new OrganizationRequestCollection() { }
                };
                foreach (var entity in collection.Entities)
                {
                    var request = new OrganizationRequest(_actionName)
                    {
                        ["Target"] = entity.ToEntityReference()
                    };
                    request.Parameters.AddRange(parameters);
                    requests.Requests.Add(request);
                }                                                                      

                // Выполнение действий.
                var response = (ExecuteMultipleResponse)context.Service.Execute(requests);
                if (response.IsFaulted)
                {
                    var faultResponse = response.Responses.FirstOrDefault(r => r.Fault != null);

                    var message = string.Empty;
                    var fault = faultResponse.Fault;
                    do
                    {
                        message += " " + fault.Message + (fault.Message.EndsWith(".") ? "" : ".");
                        fault = fault.InnerFault;
                    }
                    while (fault != null);


                    throw new InvalidWorkflowExecutionException($"A record of the \"{collection.EntityName}\" type with the ID \"{((EntityReference)requests.Requests[faultResponse.RequestIndex]["Target"]).Id}\" returned an error during execution of the action {(string.IsNullOrEmpty(actionRef.Name) ? "" : $"\"{actionRef.Name}\" ")}with the ID \"{actionRef.Id}\".{message}");
                    SetError(context, $"A record of the \"{collection.EntityName}\" type with the ID \"{((EntityReference)requests.Requests[faultResponse.RequestIndex]["Target"]).Id}\" returned an error during execution of the action {(string.IsNullOrEmpty(actionRef.Name) ? "" : $"\"{actionRef.Name}\" ")}with the ID \"{actionRef.Id}\".");
                    return;
                }

                // Организация чтения следующей страницы.
                moreRecords = collection.MoreRecords;
                if (moreRecords)
                {
                    page++;
                    var fetchXml = XDocument.Parse(fetchXmlStr);
                    fetchXml.Root.SetAttributeValue("paging-cookie", System.Security.SecurityElement.Escape(collection.PagingCookie));
                    fetchXml.Root.SetAttributeValue("page", page);
                    query = fetchXml.ToString();
                }
            } while (moreRecords);
        }


        private void RetrieveActionProperties(IOrganizationService service, EntityReference actionRef, out string actionLogicalName, out Dictionary<string, Type> properties)
        {
            // Получаем название действия.
            var query = $@"
<fetch top='1' no-lock='true'>
  <entity name='workflow'>
    <attribute name='xaml' />
    <filter>
      <condition attribute='workflowid' operator='eq' value='{actionRef.Id}'/>
    </filter>
    <link-entity name='sdkmessage' from='sdkmessageid' to='sdkmessageid' alias='Message'>
      <attribute name='name' />
    </link-entity>
  </entity>
</fetch>";
            var action = service.RetrieveMultiple(new FetchExpression(query)).Entities.FirstOrDefault();
            actionLogicalName = action.GetAttributeValue<AliasedValue>("Message.name").Value.ToString();

            // Получаем список входных параметров действия.
            var xaml = action.GetAttributeValue<string>("xaml");
            var xDoc = XDocument.Parse(xaml);
            var xProperties = xDoc.Root.Elements()
                .FirstOrDefault(e => e.Name.LocalName == "Members").Elements()
                .Where(e => e.Name.LocalName == "Property");
            properties = new Dictionary<string, Type>();
            foreach (var xProperty in xProperties)
            {
                var name = xProperty.Attribute("Name").Value;
                var typeStr = xProperty.Attribute("Type").Value;
                if (name == "InputEntities" || name == "CreatedEntities" || !typeStr.StartsWith("InArgument"))
                    continue;
                typeStr = Regex.Match(typeStr, "InArgument[(](?<type>.+)[)]").Groups["type"].Value;
                var typeParts = typeStr.Split(new char[] { ':' }, 2);
                typeStr = typeParts.Length == 1 ? typeStr : typeParts[1];
                var type = _crmTypes[typeStr];
                properties.Add(name, type);
            }
        }
    }
}