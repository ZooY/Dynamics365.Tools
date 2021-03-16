using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Activities;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.MathTools.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.MathTools.Workflow
{
    [TestClass]
    public class RoundTests
    {
        [TestMethod]
        public void Success()
        {
            var action = new Round();
            var invoker = new WorkflowInvoker(action);
            invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(new FakeOrganizationService()));

            var result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.22m },
                { "Digits", 1 }
            });
            Assert.AreEqual(2.2m, result["RoundValue"]);
            Assert.AreEqual(2.3m, result["CeilingValue"]);
            Assert.AreEqual(2.2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.22m },
                { "Digits", 0 }
            });
            Assert.AreEqual(2m, result["RoundValue"]);
            Assert.AreEqual(3m, result["CeilingValue"]);
            Assert.AreEqual(2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.22m },
                { "Digits", 2 }
            });
            Assert.AreEqual(2.22m, result["RoundValue"]);
            Assert.AreEqual(2.22m, result["CeilingValue"]);
            Assert.AreEqual(2.22m, result["FloorValue"]);
            
            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.22m },
                { "Digits", 3 }
            });
            Assert.AreEqual(2.22m, result["RoundValue"]);
            Assert.AreEqual(2.22m, result["CeilingValue"]);
            Assert.AreEqual(2.22m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.26m },
                { "Digits", 1 }
            });
            Assert.AreEqual(2.3m, result["RoundValue"]);
            Assert.AreEqual(2.3m, result["CeilingValue"]);
            Assert.AreEqual(2.2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.25m },
                { "Digits", 1 }
            });
            Assert.AreEqual(2.3m, result["RoundValue"]);
            Assert.AreEqual(2.3m, result["CeilingValue"]);
            Assert.AreEqual(2.2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.2m },
                { "Digits", 0 }
            });
            Assert.AreEqual(2m, result["RoundValue"]);
            Assert.AreEqual(3m, result["CeilingValue"]);
            Assert.AreEqual(2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", 2.5m },
                { "Digits", 0 }
            });
            Assert.AreEqual(3m, result["RoundValue"]);
            Assert.AreEqual(3m, result["CeilingValue"]);
            Assert.AreEqual(2m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", -2.22m },
                { "Digits", 1 }
            });
            Assert.AreEqual(-2.2m, result["RoundValue"]);
            Assert.AreEqual(-2.2m, result["CeilingValue"]);
            Assert.AreEqual(-2.3m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", -1.22m },
                { "Digits", 1 }
            });
            Assert.AreEqual(-1.2m, result["RoundValue"]);
            Assert.AreEqual(-1.2m, result["CeilingValue"]);
            Assert.AreEqual(-1.3m, result["FloorValue"]);
            
            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", -1.25m },
                { "Digits", 1 }
            });
            Assert.AreEqual(-1.3m, result["RoundValue"]);
            Assert.AreEqual(-1.2m, result["CeilingValue"]);
            Assert.AreEqual(-1.3m, result["FloorValue"]);

            result = invoker.Invoke(new Dictionary<string, object>
            {
                { "Value", -1.15m },
                { "Digits", 1 }
            });
            Assert.AreEqual(-1.2m, result["RoundValue"]);
            Assert.AreEqual(-1.1m, result["CeilingValue"]);
            Assert.AreEqual(-1.2m, result["FloorValue"]);
        }
    }
}