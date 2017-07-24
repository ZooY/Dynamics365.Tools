using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using PZone.Common.Testing;
using PZone.Common.Workflow.Testing;
using PZone.VariableTools.Workflow;


namespace PZone.Tests.VariableTools.Workflow
{
    [TestClass]
    public class VariableToolsTest
    {

        [TestMethod]
        public void FullTest()
        {
            var service = new FakeOrganizationService();
            var context = new FakeWorkflowContext();

            var setValueAction = new SetValue();
            var invoker = new WorkflowInvoker(setValueAction);
            invoker.Extensions.Add<ITracingService>(() => new FakseTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
            invoker.Invoke(new Dictionary<string, object>
            {
                { "DatasetName", "MySet" },
                { "VariableName", "MySimpleValue" },
                { "VariableValue", "123" }
            });
            invoker.Invoke(new Dictionary<string, object>
            {
                { "DatasetName", "MySet" },
                { "VariableName", "MyComplexNode.MyComplexValue" },
                { "VariableValue", "Value 1" }
            });
            invoker.Invoke(new Dictionary<string, object>
            {
                { "DatasetName", "MySet" },
                { "VariableName", "MyComplexNode.MyOtherComplexValue" },
                { "VariableValue", "Value 2" }
            });

            var toJsonAction = new ToJson();
            invoker = new WorkflowInvoker(toJsonAction);
            invoker.Extensions.Add<ITracingService>(() => new FakseTracingService());
            invoker.Extensions.Add<IWorkflowContext>(() => context);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => new FakeOrganizationServiceFactory(service));
            var result = invoker.Invoke(new Dictionary<string, object>
            {
                { "DatasetName", "MySet" }
            });

            Assert.AreEqual(false, result["HasError"]);
            Assert.IsNull(result["ErrorMessage"]);

            var json = result["JsonString"].ToString();
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            Assert.AreEqual("123", obj["MySimpleValue"]);

            var complex = ((Newtonsoft.Json.Linq.JObject)obj["MyComplexNode"]).ToObject<Dictionary<string, object>>();
            Assert.AreEqual("Value 1", complex["MyComplexValue"]);
            Assert.AreEqual("Value 2", complex["MyOtherComplexValue"]);
        }
    }
}