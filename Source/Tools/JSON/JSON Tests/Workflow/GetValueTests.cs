using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.JsonTools.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.JsonTools.Workflow
{
    [TestClass]
    public class GetValueTests
    {
        [TestMethod]
        public void Success()
        {
            var action = new GetValue();
            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(new FakeOrganizationService()));

            var result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Json", "{ \"Message\":\"OK\"}" },
                { "ValuePath", "Message" }
            });
            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);
            Assert.AreEqual(true, result["HasValue"]);
            Assert.AreEqual("OK", result["StringValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Json", "{ \"Complex\": { \"Message\":\"OK\"} }" },
                { "ValuePath", "Complex.Message" }
            });
            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);
            Assert.AreEqual(true, result["HasValue"]);
            Assert.AreEqual("OK", result["StringValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Json", "{ \"Complex\": { \"Message\":\"OK\"} }" },
                { "ValuePath", "Complex" }
            });
            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);
            Assert.AreEqual(true, result["HasValue"]);
            Assert.AreEqual("{\r\n  \"Message\": \"OK\"\r\n}", result["StringValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Json", "{ \"Message\":\"OK\"}" },
                { "ValuePath", "NoMessage" }
            });
            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);
            Assert.AreEqual(false, result["HasValue"]);
        }
    }
}