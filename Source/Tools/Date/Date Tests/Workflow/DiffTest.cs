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
    public class DiffTest
    {
        private readonly WorkflowInvoker _invoker;


        [TestMethod]
        public void Milliseconds()
        {
            var result = Invoke(new DateTime(2016, 11, 15, 10, 27, 35, 10), new DateTime(2016, 11, 15, 10, 27, 35, 99));
            Assert.AreEqual(89, result["DifferenceInWholeMilliseconds"]);

            result = Invoke(new DateTime(2016, 11, 15, 10, 27, 35, 10), new DateTime(2016, 11, 15, 10, 27, 36, 20));
            Assert.AreEqual(1010, result["DifferenceInWholeMilliseconds"]);
        }


        [TestMethod]
        public void Seconds()
        {
            var result = Invoke(new DateTime(2016, 11, 15, 10, 27, 35, 10), new DateTime(2016, 11, 15, 10, 27, 36, 99));
            Assert.AreEqual(1.089, result["DifferenceInSeconds"]);
            Assert.AreEqual(1, result["DifferenceInWholeSeconds"]);
        }


        [TestMethod]
        public void Months()
        {
            var result = Invoke(new DateTime(1982, 01, 02, 17, 0, 0), new DateTime(2016, 11, 15, 13, 01, 36, 99));
            Assert.AreEqual(418, result["DifferenceInWholeMonths"]);
        }


        public DiffTest()
        {
            var action = new Diff();
            _invoker = new WorkflowInvoker(action);
            _invoker.Extensions.Add<ITracingService>(() => new FakeTracingService());
            _invoker.Extensions.Add<IWorkflowContext>(() => new FakeWorkflowContext());
            _invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory());
        }


        private IDictionary<string, object> Invoke(DateTime firstDate, DateTime secondDate)
        {
            return _invoker.Invoke(new Dictionary<string, object>
            {
                { "FirstDate", firstDate },
                { "SecondDate", secondDate }
            });
        }
    }
}