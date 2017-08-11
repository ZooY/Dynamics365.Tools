using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm;
using PZone.Xrm.Workflow;


namespace PZone.EntityTools.Workflow
{
    /// <summary>
    /// ƒействие возвращает сущности "ќчередь" (queue) и "Ёлемент очереди" (queueitem) дл€ указанного GUID.
    /// </summary>
    public class GuidToQueue : GuidToEntity
    {
        /// <summary>
        /// ќчередь.
        /// </summary>
        [Output("Queue")]
        [ReferenceTarget(Metadata.Queue.LogicalName)]
        public OutArgument<EntityReference> Queue { get; set; }


        /// <summary>
        /// Ёлемент очереди.
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