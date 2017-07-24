using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;


namespace PZone.Common.Workflow
{
    /// <summary>
    /// �������� ���������� �������� ��������.
    /// </summary>
    public class Context
    {
        private readonly Lazy<IWorkflowContext> _context;
        private readonly Lazy<IOrganizationService> _service;
        private readonly Lazy<ITracingService> _tracingService;

        /// <summary>
        /// ���� �������� ����������� ��� ���
        /// </summary>
        public Lazy<bool> IsTestingOrganization;


        /// <summary>
        /// ���������� ������.
        /// </summary>
        /// <param name="activityContext">�������� ���������� �������� ��������.</param>
        public Context(CodeActivityContext activityContext)
        {
            SourceActivityContext = activityContext;
            _context = new Lazy<IWorkflowContext>(() => SourceActivityContext.GetContext());
            _service = new Lazy<IOrganizationService>(() => SourceActivityContext.GetService());
            _tracingService = new Lazy<ITracingService>(() => SourceActivityContext.GetTracingService());
        }


        /// <summary>
        /// �������� ����������� �������� ���������� �������� ��������.
        /// </summary>
        public CodeActivityContext SourceActivityContext { get; }


        /// <summary>
        /// ������ �� ��������� CRM-�������, ������������ �� ����� ������������, ��������������� ������ �������� ��������.
        /// </summary>
        public IOrganizationService Service => _service.Value;


        /// <summary>
        /// C����� �����������.
        /// </summary>
        public ITracingService TracingService => _tracingService.Value;


        /// <summary>
        /// �������� ����������� �������� �������� ��������.
        /// </summary>
        public IWorkflowContext SourceContext => _context.Value;


        /// <summary>
        /// ��������� ��� �������� ��������.
        /// </summary>
        public string EntityName => _context.Value.PrimaryEntityName;


        /// <summary>
        /// ������������� �������� ��������.
        /// </summary>
        public Guid EntityId => _context.Value.PrimaryEntityId;
    }
}