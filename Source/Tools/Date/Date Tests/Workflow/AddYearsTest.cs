using System;
using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.DateTools.Workflow;
using PZone.Xrm.Testing;
using PZone.Xrm.Testing.Workflow;


namespace PZone.Tests.DateTools.Workflow
{
    [TestClass]
    public class AddYearsTest
    {
        private readonly WorkflowInvoker _invoker;


        [TestMethod]
        public void Success()
        {
            var result = Invoke(new DateTime(1982, 1, 2, 10, 27, 35, 10), 45);
            Assert.AreEqual(new DateTime(2027, 1, 2, 10, 27, 35, 10), result["Result"]);
        }


        public AddYearsTest()
        {
            var action = new AddYears();
            _invoker = new WorkflowInvoker(action);
            _invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            _invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            _invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory());
        }

        private IDictionary<string, object> Invoke(DateTime date, int count)
        {
            return _invoker.Invoke(new Dictionary<string, object>
            {
                { "Date", date },
                { "Count", count }
            });
        }
    }
}