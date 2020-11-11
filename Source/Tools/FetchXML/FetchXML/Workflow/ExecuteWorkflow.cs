using System.Activities;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Запуск Workflow по результатам выполнения FetchXML.
    /// </summary>
    public class ExecuteWorkflow : WorkflowBase
    {
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
        [Input("Workflow")]
        [ReferenceTarget("workflow")]
        // [Output("Input FetchXML string")]
        public InArgument<EntityReference> Workflow { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var fetchXmlStr = FetchXml.Get(context);
            var workflowRef = Workflow.Get(context);

            var moreRecords = false;
            int page = 1;
            string query = fetchXmlStr;
            do
            {
                var collection = context.SystemService.RetrieveMultiple(new FetchExpression(query));
                if (collection.Entities.Count < 1)
                    break;

                var requests = new ExecuteMultipleRequest()
                {
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    Requests = new OrganizationRequestCollection() { }
                };
                requests.Requests.AddRange(collection.Entities.Select(e =>
                        new ExecuteWorkflowRequest()
                        {
                            WorkflowId = workflowRef.Id,
                            EntityId = e.Id
                        }
                    ).ToList());
                var response = (ExecuteMultipleResponse)context.Service.Execute(requests);
                if (response.IsFaulted)
                {
                    var faultResponse = response.Responses.FirstOrDefault(r => r.Fault != null);
                    var faultWorkflowRequest = (ExecuteWorkflowRequest)requests.Requests[faultResponse.RequestIndex];
                    SetError(context, $"A record of the \"{collection.EntityName}\" type with the ID \"{faultWorkflowRequest.EntityId}\" returned an error during execution of the workflow {(string.IsNullOrEmpty(workflowRef.Name) ? "" : $"\"{workflowRef.Name}\" ")}with the ID \"{workflowRef.Id}\".");
                    return;
                }

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
    }
}