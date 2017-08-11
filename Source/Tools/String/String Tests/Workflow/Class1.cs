using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.StringTools.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.StringTools.Workflow
{
    [TestClass]
    public class SubstringTest
    {


        [TestMethod]
        public void SubstringWorkflowTest_CheckSubstring()
        {
            var action = new Substring();

            var service = new FakeOrganizationService();
            var factory = new FakeOrganizationServiceFactory(service);
            var context = new FakeWorkflowContext();

            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);

            var result = invoker.Invoke(new Dictionary<string, object>
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
            var action = new Substring();

            var service = new FakeOrganizationService();
            var factory = new FakeOrganizationServiceFactory(service);
            var context = new FakeWorkflowContext();

            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);

            var result = invoker.Invoke(new Dictionary<string, object>
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
            var action = new Substring();

            var service = new FakeOrganizationService();
            var factory = new FakeOrganizationServiceFactory(service);
            var context = new FakeWorkflowContext();

            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);

            var result = invoker.Invoke(new Dictionary<string, object>
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
            var action = new Substring();

            var service = new FakeOrganizationService();
            var factory = new FakeOrganizationServiceFactory(service);
            var context = new FakeWorkflowContext();

            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);

            var result = invoker.Invoke(new Dictionary<string, object>
            {
                ["String"] = "test",
                ["StartIndex"] = 3,
                ["Length"] = 2
            });

            Assert.AreEqual(result["HasError"], true);
        }
    }
}