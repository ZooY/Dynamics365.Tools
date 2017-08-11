using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// �������� ���������� �������� "�������" (queue) � "������� �������" (queueitem) ��� ���������� GUID.
    /// </summary>
    public class GuidToQueue : GuidToEntity
    {
        /// <summary>
        /// �������.
        /// </summary>
        [Output("Queue")]
        [ReferenceTarget(Metadata.Queue.LogicalName)]
        public OutArgument<EntityReference> Queue { get; set; }


        /// <summary>
        /// ������� �������.
        /// </summary>
        [Output("Queue Item")]
        [ReferenceTarget(Metadata.QueueItem.LogicalName)]
        public OutArgument<EntityReference> QueueItem { get; set; }


        protected override void Execute(Context context)
        {
            SetValue(context, Queue, Metadata.Queue.LogicalName);
            SetValue(context, QueueItem, Metadata.QueueItem.LogicalName);
        }
    }
}