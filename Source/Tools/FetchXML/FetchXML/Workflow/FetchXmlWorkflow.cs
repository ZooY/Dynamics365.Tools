using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.FetchXmlTools.Workflow
{
    /// <summary>
    /// Действие процесса, содержащее запрос FetchXML.
    /// </summary>
    public abstract class FetchXmlWorkflow : WorkflowBase
    {
        /// <summary>
        /// Запрос FetchXML, получающий список записей.
        /// </summary>
        [RequiredArgument]
        [Input("FetchXML Query")]
        public InArgument<string> FetchXml { get; set; }
    }
}