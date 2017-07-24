//using System.Activities;
//using System.Linq;
//using Microsoft.Crm.Sdk.Messages;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Messages;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;
//using PZone.Common.Workflow;


//namespace PZone.FetchXmlTools.Workflow
//{
//    /// <summary>
//    /// Запуск воркфлоу из фетча
//    /// </summary>
//    public class ExecuteWorkflowFromFetch : WorkflowBase
//    {
//        /// <summary>
//        /// Запрос FetchXML.
//        /// </summary>
//        [RequiredArgument]
//        [Input("FetchXML string")]
//        // [Output("Input FetchXML string")]
//        public InArgument<string> FetchXmlString { get; set; }

//        /// <summary>
//        /// Workflow
//        /// </summary>
//        [RequiredArgument]
//        [Input("Workflow")]
//        [ReferenceTarget("workflow")]
//        // [Output("Input FetchXML string")]
//        public InArgument<EntityReference> Workflow { get; set; }

//        /// <inheritdoc />
//        protected override void Execute(Context context)
//        {
//            var query = FetchXmlString.Get(context);
//            if (string.IsNullOrWhiteSpace(query))
//                return;

//            var pageNumber = 1;
//            var errors = "";

//            do
//            {
//                var result = ExecuteFetch(query, pageNumber++, 5000, context.Service);
//                if (result.Entities.Count < 1)
//                    break;

//                errors += ExecuteWf(result, context);
//                if (!result.MoreRecords)
//                    break;

//            } while (true);

//            if (errors.Any())
//                SetError(context, errors);
//        }


//        private EntityCollection ExecuteFetch(string fetch, int pageNumber, int count, IOrganizationService service)
//        {
//            var conversionRequest = new FetchXmlToQueryExpressionRequest
//            {
//                FetchXml = fetch
//            };

//            var conversionResponse = (FetchXmlToQueryExpressionResponse)service.Execute(conversionRequest);
//            var queryExpression = conversionResponse.Query;
//            queryExpression.PageInfo = new PagingInfo()
//            {
//                Count = count,
//                PageNumber = pageNumber
//            };

//            return service.RetrieveMultiple(queryExpression);
//        }


//        private string ExecuteWf(EntityCollection entities, Context context)
//        {
//            var requests = new ExecuteMultipleRequest()
//            {
//                Settings = new ExecuteMultipleSettings()
//                {
//                    ContinueOnError = true,
//                    ReturnResponses = false
//                },
//                Requests = new OrganizationRequestCollection() { }
//            };

//            requests.Requests.AddRange(entities.Entities.Select(e =>
//                    new ExecuteWorkflowRequest()
//                    {
//                        WorkflowId = Workflow.Get(context).Id,
//                        EntityId = e.Id
//                    }
//                ).ToList());
//            var responseWithResults = (ExecuteMultipleResponse)context.Service.Execute(requests);

//            return string.Join(", ", responseWithResults.Responses.Where(r => r.Fault != null).Select(r => r.Fault.Message));
//        }
//    }
//}