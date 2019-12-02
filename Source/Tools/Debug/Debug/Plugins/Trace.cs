using System;
using System.IO;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using PZone.Xrm.Plugins;


namespace PZone.DebugTools.Plugins
{
    public class Trace : PluginBase, IPlugin
    {
        /// <inheritdoc />
        public Trace(string unsecureConfig) : base(unsecureConfig)
        {
        }


        public override IPluginResult Execute(Context context)
        {
            var maxLen = 2000;
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                Error = (s, e) => e.ErrorContext.Handled = true
            };
            var inputParameters = JsonConvert.SerializeObject(context.SourceContext.InputParameters, jsonSettings);
            var outputParameters = JsonConvert.SerializeObject(context.SourceContext.OutputParameters, jsonSettings);
            var sharedVariables = JsonConvert.SerializeObject(context.SourceContext.SharedVariables, jsonSettings);
            context.Service.Create(new Entity("pz_plugin_trace")
            {
                ["pz_name"] = $"{context.Stage.GetDisplayName()} {context.SourceContext.MessageName} {context.SourceContext.PrimaryEntityName} {DateTime.Now:yyyy-MM-dd HH:mm:ss.fffffff}",
                ["pz_stage"] = new OptionSetValue(419360000 + context.SourceContext.Stage),
                ["pz_message"] = context.SourceContext.MessageName,
                ["pz_entity"] = context.SourceContext.PrimaryEntityName,
                ["pz_mode"] = new OptionSetValue(419360000 + context.SourceContext.Mode),
                ["pz_userid"] = new EntityReference("systemuser", context.SourceContext.UserId),
                ["pz_initiating_userid"] = new EntityReference("systemuser", context.SourceContext.InitiatingUserId),
                ["pz_correlation_id"] = context.SourceContext.CorrelationId.ToString(),
                ["pz_depth"] = context.SourceContext.Depth,
                ["pz_input_parameters"] = inputParameters.Length > maxLen ? inputParameters.Substring(0, maxLen - 3) + "..." : inputParameters,
                ["pz_output_parameters"] = outputParameters.Length > maxLen ? outputParameters.Substring(0, maxLen - 3) + "..." : outputParameters,
                ["pz_shared_variables"] = sharedVariables.Length > maxLen ? sharedVariables.Substring(0, maxLen - 3) + "..." : sharedVariables
            });
            return Ok();
        }
    }
}