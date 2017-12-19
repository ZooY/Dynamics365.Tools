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
    public class Substring
    {
        private readonly WorkflowInvoker _invoker;


        public Substring()
        {
            var action = new PZone.StringTools.Workflow.Substring();
            var service = new FakeOrganizationService();

            _invoker = new WorkflowInvoker(action);
            _invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            _invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            _invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
        }


        [TestMethod]
        public void SubstringWorkflowTest_CheckSubstring()
        {
            var result = _invoker.Invoke(new Dictionary<string, object>
            {
                ["String"] = "test",
                ["StartIndex"] = 0,
                ["Length"] = 2
            });
            Assert.AreEqual(result["String"], "te");
        }


        [TestMethod]
        public void SubstringWorkflowTest_CheckOverflow()
        {
            var result = _invoker.Invoke(new Dictionary<string, object>
            {
                ["String"] = "test",
                ["StartIndex"] = 0,
                ["Length"] = 6
            });
            Assert.AreEqual(result["HasError"], true);
        }


        [TestMethod]
        public void SubstringWorkflowTest_CheckStartIndexOverflow()
        {
            var result = _invoker.Invoke(new Dictionary<string, object>
            {
                ["String"] = "test",
                ["StartIndex"] = 6,
                ["Length"] = 7
            });
            Assert.AreEqual(result["HasError"], true);
        }


        [TestMethod]
        public void SubstringWorkflowTest_CheckStartIndexGtLength()
        {
            var result = _invoker.Invoke(new Dictionary<string, object>
            {
                ["String"] = "test",
                ["StartIndex"] = 3,
                ["Length"] = 2
            });
            Assert.AreEqual(result["HasError"], true);
        }
    }
}