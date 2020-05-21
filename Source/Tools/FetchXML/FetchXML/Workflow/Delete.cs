using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    public class Delete : FetchXmlWorkflow
    {
        /// <summary>
        /// Количество найденных записей.
        /// </summary>
        [Output("Finded Count")]
        public OutArgument<int> FindedCount { get; set; }


        /// <summary>
        /// Количество удаленных записей.
        /// </summary>
        [Output("Deleted Count")]
        public OutArgument<int> DeletedCount { get; set; }


        protected override void Execute(Context context)
        {
            var query = FetchXml.Get(context);
            if (string.IsNullOrWhiteSpace(query))
                return;

            var entities = context.Service.RetrieveMultiple(query).Entities;

            FindedCount.Set(context, entities.Count);

            if (entities.Count == 0)
            {
                DeletedCount.Set(context, 0);
                return;
            }

            var request = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };
            request.Requests.AddRange(entities.Select(entity => new DeleteRequest
            {
                Target = entity.ToEntityReference()
            }));
            
            var responses = (ExecuteMultipleResponse)context.Service.Execute(request);
            if (!responses.IsFaulted)
            {
                DeletedCount.Set(context, entities.Count);
                return;
            }

            var deletedCount = 0;
            var faults = new List<string>();
            foreach (var response in responses.Responses)
            {
                if (response.Fault == null)
                    deletedCount++;
                else
                    faults.Add(response.Fault.Message);
            }
            DeletedCount.Set(context, deletedCount);
            throw new Exception(string.Join(" ", faults));
        }
    }
}