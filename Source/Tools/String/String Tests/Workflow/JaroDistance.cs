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
    public class JaroDistance
    {
        private readonly WorkflowInvoker _invoker;


        public JaroDistance()
        {

            var action = new PZone.StringTools.Workflow.JaroDistance();
            var service = new FakeOrganizationService();

            _invoker = new WorkflowInvoker(action);
            _invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            _invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            _invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
        }


        [TestMethod]
        public void Success()
        {
            var result = _invoker.Invoke(new Dictionary<string, object> { ["String1"] = "MARTHA", ["String2"] = "MARHTA" });
            Assert.AreEqual(94, result["Result"]);

            result = _invoker.Invoke(new Dictionary<string, object> { ["String1"] = "DIXON", ["String2"] = "DICKSONX" });
            Assert.AreEqual(77, result["Result"]);

            result = _invoker.Invoke(new Dictionary<string, object> { ["String1"] = "CRATE", ["String2"] = "TRACE" });
            Assert.AreEqual(73, result["Result"]);
        }
    }
}
