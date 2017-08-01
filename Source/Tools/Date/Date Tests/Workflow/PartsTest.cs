using System;
using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Common.Testing;
using PZone.Common.Workflow.Testing;
using PZone.DateTools.Workflow;


namespace PZone.Tests.DateTools.Workflow
{
    [TestClass]
    public class PartsTest
    {
        [TestMethod]
        public void CorrectDate()
        {
            var action = new Parts();

            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakseTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory());

            var result = invoker.Invoke(new Dictionary<string, object>
            {
                { "InputDateTime", DateTime.Parse("2016-11-15 10:27:35") }
            });

            Assert.AreEqual(DateTime.Parse("2016-11-15 00:00:00"), result["DateAsDT"]);
            Assert.AreEqual(DateTime.Parse("0001-01-01 10:27:35"), result["TimeAsDT"]);
            Assert.AreEqual(15, result["Day"]);
            Assert.AreEqual(11, result["Month"]);
            Assert.AreEqual(2016, result["Year"]);
            Assert.AreEqual(10, result["Hour"]);
            Assert.AreEqual(27, result["Minute"]);
            Assert.AreEqual(35, result["Second"]);
            Assert.AreEqual("Unspecified", result["Kind"]);
        }
    }
}
