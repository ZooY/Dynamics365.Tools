using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;


namespace PZone.Common.Workflow.Testing
{
    public class FakeWorkflowContext : IWorkflowContext
    {
        public int Mode { get; }
        public int IsolationMode { get; }
        public int Depth { get; }
        public string MessageName { get; }
        public string PrimaryEntityName { get; }
        public Guid? RequestId { get; }
        public string SecondaryEntityName { get; }
        public ParameterCollection InputParameters { get; }
        public ParameterCollection OutputParameters { get; }
        public ParameterCollection SharedVariables { get; } = new ParameterCollection();
        public Guid UserId { get; } = new Guid();
        public Guid InitiatingUserId { get; }
        public Guid BusinessUnitId { get; }
        public Guid OrganizationId { get; }
        public string OrganizationName { get; }
        public Guid PrimaryEntityId { get; }
        public EntityImageCollection PreEntityImages { get; }
        public EntityImageCollection PostEntityImages { get; }
        public EntityReference OwningExtension { get; }
        public Guid CorrelationId { get; }
        public bool IsExecutingOffline { get; }
        public bool IsOfflinePlayback { get; }
        public bool IsInTransaction { get; }
        public Guid OperationId { get; }
        public DateTime OperationCreatedOn { get; }
        public string StageName { get; }
        public int WorkflowCategory { get; }
        public int WorkflowMode { get; }
        public IWorkflowContext ParentContext { get; }
    }
}
