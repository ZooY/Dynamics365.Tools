using PZone.Common.Workflow;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Удаление переменной.
    /// </summary>
    public class RemoveValue : VariableBase
    {
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var key = GetKey(context);
            if (context.SourceContext.SharedVariables.ContainsKey(key))
                context.SourceContext.SharedVariables.Remove(key);
        }
    }
}