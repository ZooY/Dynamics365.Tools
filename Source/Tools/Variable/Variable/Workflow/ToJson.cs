using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using PZone.Xrm.Workflow;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Формирование, на основе всех переменных набора, строки в формате JSON.
    /// </summary>
    public class ToJson : DatasetBase
    {
        [Output("JSON")]
        public OutArgument<string> JsonString { get; set; }


        protected override void Execute(Context context)
        {
            var findKey = GetDatasetName(context) + ".";
            var keys = context.SourceContext.SharedVariables.Keys.Where(k => k.StartsWith(findKey));
            var result = new Dictionary<string, object>();
            foreach (var key in keys)
            {
                var value = context.SourceContext.SharedVariables[key];
                var names = key.Split('.').Skip(1).ToArray();
                var node = result;
                if (names.Length > 1)
                    foreach (var name in names.Take(names.Length - 1))
                    {
                        if (!node.ContainsKey(name))
                            node.Add(name, new Dictionary<string, object>());
                        node = (Dictionary<string, object>)node[name];
                    }
                node.Add(names.Last(), value);
            }
            var json = JsonConvert.SerializeObject(result);
            JsonString.Set(context, json);
        }
    }
}