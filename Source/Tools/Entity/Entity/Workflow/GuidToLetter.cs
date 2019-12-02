using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// Действие возвращает сущность "Письмо" (letter) для указанного GUID.
    /// </summary>
    public class GuidToLetter : GuidToEntity
    {
        /// <summary>
        /// Письмо.
        /// </summary>
        [Output("Letter")]
        [ReferenceTarget("letter")]
        public OutArgument<EntityReference> Letter { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Letter, "letter");
        }
    }
}