using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.StringTools.Workflow
{
    [TestClass]
    public class JaroWinklerDistance
    {
        private readonly WorkflowInvoker _invoker;


        public JaroWinklerDistance()
        {
            var action = new PZone.StringTools.Workflow.JaroWinklerDistance();
            var service = new FakeOrganizationService();

            _invoker = new WorkflowInvoker(action);
            _invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            _invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            _invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
        }


        [TestMethod]
        public void Success()
        {
            var result = _invoker.Invoke(new Dictionary<string, object> { ["String1"] = "MARTHA", ["String2"] = "MARHTA", ["CaseSensitive"] = true, ["AccentSensitive"] = false });
            Assert.AreEqual(96, result["Result"]);

            result = _invoker.Invoke(new Dictionary<string, object> { ["String1"] = "DIXON", ["String2"] = "DICKSONX", ["CaseSensitive"] = true, ["AccentSensitive"] = false });
            Assert.AreEqual(81, result["Result"]);
        }
    }
}