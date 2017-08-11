using System.Activities;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;
using PZone.Xrm.Workflow.Exceptions;


namespace PZone.VariableTools.Workflow
{
    /// <summary>
    /// Общий класс для компонентов, работающих с переменными.
    /// </summary>
    public abstract class VariableBase : DatasetBase
    {
        /// <summary>
        /// Имя переменной
        /// </summary>
        [Input("Variable Name")]
        [RequiredArgument]
        public InArgument<string> VariableName { get; set; }


        /// <summary>
        /// Получение имени переменной из полного имени, включающего имя набора.
        /// </summary>
        protected string GetVariableName(Context context)
        {
            var variableName = VariableName.Get(context);
            if (string.IsNullOrWhiteSpace(variableName))
                throw new InvalidWorkflowExecutionException("Variable Name is empty.");
            var regexResult = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)*$").Match(variableName);
            if (!regexResult.Success)
                throw new InvalidWorkflowExecutionException($"Incorrect Variable Name \"{variableName}\".");
            variableName = variableName.Trim();
            return variableName;
        }


        protected string GetKey(Context context)
        {
            var datasetName = GetDatasetName(context);
            var variableName = GetVariableName(context);
            return $"{datasetName}.{variableName}";
        }
    }
}