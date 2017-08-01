using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;


namespace PZone.Xrm.Sdk
{
    /// <summary>
    /// Расширение функционала классов, реализующих интерфейс <see cref="ITracingService"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ITracingServiceWorkflowExtension
    {
        /// <summary>
        /// Запись в трассировку данных об исключении.
        /// </summary>
        /// <param name="service">Экземпляр сервиса трассеровки.</param>
        /// <param name="context">Объект для записи в трассировку.</param>
        public static void Trace(this ITracingService service, IWorkflowContext context)
        {
            var contextData = new
            {
                MessageName = context.MessageName,
                StageName = context.StageName,
                Mode = context.Mode,
                UserId = context.UserId,
                InitiatingUserId = context.InitiatingUserId,
                BusinessUnitId = context.BusinessUnitId,
                OrganizationId = context.OrganizationId,
                OrganizationName = context.OrganizationName,
                CorrelationId = context.CorrelationId,
                Depth = context.Depth,
                PrimaryEntityId = context.PrimaryEntityId,
                PrimaryEntityName = context.PrimaryEntityName,
                SecondaryEntityName = context.SecondaryEntityName,
                IsExecutingOffline = context.IsExecutingOffline,
                IsInTransaction = context.IsInTransaction,
                IsOfflinePlayback = context.IsOfflinePlayback,
                IsolationMode = context.IsolationMode,
                OperationCreatedOn = context.OperationCreatedOn,
                RequestId = context.RequestId,
                OperationId = context.OperationId,
                ParentContext = context.ParentContext,
                InputParameters = context.InputParameters,
                OutputParameters = context.OutputParameters,
                OwningExtension = context.OwningExtension,
                PreEntityImages = context.PreEntityImages,
                PostEntityImages = context.PostEntityImages,
                WorkflowCategory = context.WorkflowCategory,
                WorkflowMode = context.WorkflowMode,
                SharedVariables = context.SharedVariables
            };
            service.Trace(contextData);
        }
    }
}