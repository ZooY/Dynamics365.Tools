using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Установка значения переменной.
    /// </summary>
    public class SetValue : VariableBase
    {
        [Input("Variable Value")]
        public InArgument<string> VariableValue { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var key = GetKey(context);
            if (context.SourceContext.SharedVariables.ContainsKey(key))
                context.SourceContext.SharedVariables.Remove(key);
            context.SourceContext.SharedVariables.Add(key, VariableValue.Get(context));
        }
    }
}