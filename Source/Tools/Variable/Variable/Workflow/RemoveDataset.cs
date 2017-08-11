using System.Linq;
using PZone.Xrm.Workflow;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Удаление всех переменных, относящихся к указанному набору данных.
    /// </summary>
    public class RemoveDataset : DatasetBase
    {
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var key = GetDatasetName(context) + ".";
            var datasetKeys = context.SourceContext.SharedVariables.Keys.Where(k => k.StartsWith(key));
            foreach (var varKey in datasetKeys)
                context.SourceContext.SharedVariables.Remove(varKey);
        }
    }
}