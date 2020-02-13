using System.Activities;
using System.Web;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Parsing the dynamic URL and getting the parameters of the entity.
    /// </summary>
    public class ParseEntityDynamicUrl : WorkflowBase
    {
        /// <summary>
        /// Entity URL (dynamic).
        /// </summary>
        [Input("Entity URL (dynamic)")]
        [RequiredArgument]
        public InArgument<string> UrlString { get; set; }


        /// <summary>
        /// Entity Logical Name.
        /// </summary>
        [Output("Entity Logical Name")]
        public OutArgument<string> EntityName { get; set; }


        /// <summary>
        /// Entity Type Code.
        /// </summary>
        [Output("Entity Type Code")]
        public OutArgument<int> EntityCode { get; set; }


        /// <summary>
        /// Entity ID.
        /// </summary>
        [Output("Entity ID")]
        public OutArgument<string> IdString { get; set; }


        protected override void Execute(Context context)
        {
            var urlString = UrlString.Get(context);
            if (string.IsNullOrWhiteSpace(urlString))
                return;
            var urlParts = urlString.Split('?');
            var parameters = HttpUtility.ParseQueryString(urlParts[1]);

            var entityTypeCode = int.Parse(parameters["etc"]);
            EntityCode.Set(context, entityTypeCode);
            IdString.Set(context, parameters["id"]);


            var retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Criteria = new MetadataFilterExpression(LogicalOperator.And)
                    {
                        Conditions = { new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.Equals, entityTypeCode) }
                    },
                    Properties = new MetadataPropertiesExpression { AllProperties = false, PropertyNames = { "LogicalName" } }
                }
            };
            var response = (RetrieveMetadataChangesResponse)context.SystemService.Execute(retrieveMetadataChangesRequest);
            if (response.EntityMetadata.Count == 1)
                EntityName.Set(context, response.EntityMetadata[0].LogicalName);
        }
    }
}