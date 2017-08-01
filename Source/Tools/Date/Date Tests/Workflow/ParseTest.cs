using System;
using System.Activities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZone.DateTools.Workflow;


namespace PZone.Tests.DateTools.Workflow
{
    [TestClass]
    public class ParseTest
    {
        [TestMethod]
        public void Success()
        {
            {
                var parse = new Parse { DateString = "2014-06-10 15:17:23" };
                var result = WorkflowInvoker.Invoke(parse);
                var expected = new DateTime(2014, 6, 10, 15, 17, 23);
                var actual = (DateTime)result["Result"];
                Assert.AreEqual(expected, actual);
            }
            {
                var parse = new Parse
                {
                    DateString = "09.06.2014 16:17:23",
                    DateFormat = "dd.MM.yyyy HH:mm:ss"
                };
                var result = WorkflowInvoker.Invoke(parse);
                var expected = new DateTime(2014, 6, 9, 16, 17, 23);
                var actual = (DateTime)result["Result"];
                Assert.AreEqual(expected, actual);
            }
            {
                var parse = new Parse { DateString = "17:17:23" };
                var result = WorkflowInvoker.Invoke(parse);
                var expected = new DateTime(1, 1, 1, 17, 17, 23);
                var actual = (DateTime)result["Result"];
                Assert.AreEqual(expected, actual);
            }
            {
                var parse = new Parse { DateString = "17:09" };
                var result = WorkflowInvoker.Invoke(parse);
                var expected = new DateTime(1, 1, 1, 17, 9, 00);
                var actual = (DateTime)result["Result"];
                Assert.AreEqual(expected, actual);
            }
            {
                var parse = new Parse { DateString = "7:8" };
                var result = WorkflowInvoker.Invoke(parse);
                var expected = new DateTime(1, 1, 1, 7, 8, 00);
                var actual = (DateTime)result["Result"];
                Assert.AreEqual(expected, actual);
            }
        }
    }
}