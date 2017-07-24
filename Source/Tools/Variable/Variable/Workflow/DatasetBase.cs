using System.Activities;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Common.Workflow;
using PZone.Common.Workflow.Exceptions;
using PZone.Activities;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Общий класс для компонентов, работающих с наборами переменных.
    /// </summary>
    public abstract class DatasetBase : WorkflowBase
    {

        /// <summary>
        /// Имя набора переменных, объединенных общим смыслом и для которого можно применять групповые операции.
        /// </summary>
        [Input("Dataset Name")]
        public InArgument<string> DatasetName { get; set; }


        /// <inheritdoc />
        protected string GetDatasetName(Context context)
        {
            var datasetName = DatasetName.Get(context);
            if (string.IsNullOrWhiteSpace(datasetName))
                datasetName = "Global";
            else
            {
                datasetName = datasetName.Trim();
                var regexResult = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*$").Match(datasetName);
                if (!regexResult.Success)
                    throw new InvalidWorkflowExecutionException($"Incorrect Dataset Name \"{datasetName}\".");
            }
            return datasetName;
        }
    }
}