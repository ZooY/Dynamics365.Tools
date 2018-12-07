using System;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using PZone.Xrm;
using PZone.Xrm.Plugins;


namespace PZone.ProcessTools.Plugins
{
    public class ExecuteWorkflow : PluginBase, IPlugin
    {
        public class Config
        {
            /// <summary>
            /// Workflow Display Name
            /// </summary>
            [JsonProperty("workflowName")]
            public string WorkflowName { get; set; }

            /// <summary>
            /// Workflow ID.
            /// </summary>
            [JsonProperty("workflowId")]
            public Guid WorkflowId { get; set; }
        }


        private static Config _config;


        public ExecuteWorkflow(string unsecureConfig) : base(unsecureConfig)
        {
        }


        public override void Configuring(Context context)
        {
            _config = JsonConvert.DeserializeObject<Config>(UnsecureConfiguration);
            if (_config == null)
                throw new InvalidPluginExecutionException("Plug-in configuration error. Failed to parse confuguration string.");
            if (_config.WorkflowId != default(Guid))
                return;
            if (string.IsNullOrWhiteSpace(_config.WorkflowName))
                throw new InvalidPluginExecutionException("Plug-in configuration error. Workflow ID and Name is empty.");

            var workflows = context.Service.RetrieveMultiple($@"
<fetch top='2' no-lock='true'>
  <entity name='workflow'>
    <attribute name='workflowid' />
    <filter>
      <condition attribute='name' operator='eq' value='{_config.WorkflowName}'/>
      <condition attribute='type' operator='eq' value='1'/>
    </filter>
  </entity>
</fetch>").Entities;
            if (workflows.Count < 1)
                throw new InvalidPluginExecutionException("Plug-in configuration error. Workflow with the specified name not found.");
            if (workflows.Count > 1)
                throw new InvalidPluginExecutionException("Plug-in configuration error. Found more than one Workflow with the specified name.");
            _config.WorkflowId = workflows.First().Id;
        }


        public override IPluginResult Execute(Context context)
        {
            context.Service.Execute(new ExecuteWorkflowRequest()
            {
                WorkflowId = _config.WorkflowId,
                EntityId = context.SourceContext.PrimaryEntityId
            });
            return Ok();
        }
    }
}