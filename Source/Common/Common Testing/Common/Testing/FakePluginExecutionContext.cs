using System;
using Microsoft.Xrm.Sdk;


namespace PZone.Common.Testing
{
    /// <summary>
    /// Заглушка для классов, реализующих интерфейс <see cref="IPluginExecutionContext"/>.
    /// </summary>
    public class FakePluginExecutionContext : IPluginExecutionContext
    {
        private readonly Entity _primaryEntity;


        /// <inheritdoc />
        public int Mode { get; }


        /// <inheritdoc />
        public int IsolationMode { get; }


        /// <inheritdoc />
        public int Depth { get; }


        /// <inheritdoc />
        public string MessageName { get; }


        /// <inheritdoc />
        public Guid PrimaryEntityId => _primaryEntity.Id;


        /// <inheritdoc />
        public string PrimaryEntityName => _primaryEntity.LogicalName;


        /// <inheritdoc />
        public Guid? RequestId { get; }


        /// <inheritdoc />
        public string SecondaryEntityName { get; }


        /// <inheritdoc />
        public ParameterCollection InputParameters { get; }


        /// <inheritdoc />
        public ParameterCollection OutputParameters { get; }


        /// <inheritdoc />
        public ParameterCollection SharedVariables { get; }


        /// <inheritdoc />
        public Guid UserId { get; }


        /// <inheritdoc />
        public Guid InitiatingUserId { get; }


        /// <inheritdoc />
        public Guid BusinessUnitId { get; }


        /// <inheritdoc />
        public Guid OrganizationId { get; }


        /// <inheritdoc />
        public string OrganizationName { get; }


        /// <inheritdoc />
        public EntityImageCollection PreEntityImages { get; }


        /// <inheritdoc />
        public EntityImageCollection PostEntityImages { get; }


        /// <inheritdoc />
        public EntityReference OwningExtension { get; }


        /// <inheritdoc />
        public Guid CorrelationId { get; }


        /// <inheritdoc />
        public bool IsExecutingOffline { get; }


        /// <inheritdoc />
        public bool IsOfflinePlayback { get; }


        /// <inheritdoc />
        public bool IsInTransaction { get; }


        /// <inheritdoc />
        public Guid OperationId { get; }


        /// <inheritdoc />
        public DateTime OperationCreatedOn { get; }


        /// <inheritdoc />
        public int Stage { get; }


        /// <inheritdoc />
        public IPluginExecutionContext ParentContext { get; }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="primaryEntity">Основная сущность.</param>
        public FakePluginExecutionContext(Entity primaryEntity)
        {
            _primaryEntity = primaryEntity;
            InputParameters = new ParameterCollection
            {
                { "Target", _primaryEntity }
            };
        }
    }
}