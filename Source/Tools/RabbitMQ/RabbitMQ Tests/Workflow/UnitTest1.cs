using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.RabbitMqTools.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.RabbitMqTools.Workflow
{
    [TestClass]
    public class SendTest
    {
        [TestMethod]
        public void RealSend()
        {
            var service = new FakeOrganizationService();

            var setValueAction = new Send();
            var invoker = new WorkflowInvoker(setValueAction);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
            var result = invoker.Invoke(new Dictionary<string, object>
            {
                { "MqHost", "crmdevapp01" },
                { "MqUserName", "user" },
                { "MqUserPassword", "user" },
                { "MqExchangeName", "API" },
                { "MqRoutingKey", "Smev" },
                { "MqHeaderName", "Method" },
                { "MqHeaderValue", "EasyVerification" },
                { "Message", "{\"data\":\"OK\"}" }
            });
            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);
        }
    }
}
