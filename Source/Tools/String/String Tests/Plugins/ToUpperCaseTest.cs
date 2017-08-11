using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using PZone.StringTools.Plugins;
using PZone.Xrm.Plugins;
using PZone.Xrm.Testing;


namespace PZone.Tests.StringTools.Plugins
{
    [TestClass]
    public class ToUpperCaseTest
    {
        private const string CONFIG_STR = @"
[
    ""firstname"",
    ""lastname"",
    ""middlename""
]";


        [TestMethod]
        public void SimpleData()
        {
            var entity = new Entity("contact", Guid.NewGuid())
            {
                ["firstname"] = "РОМАН",
                ["lastname"] = "копаев-ЗаДуНайский",
                ["middlename"] = "анатольевич"
            };
            IPluginExecutionContext context = new FakePluginExecutionContext(entity);
            IServiceProvider serviceProvider = new FakeServiceProvider(context);

            var plugin = new ToUpperCase(CONFIG_STR);
            plugin.Execute(serviceProvider);

            var result = context.GetContextEntity();
            Assert.AreEqual("РОМАН", result["firstname"]);
            Assert.AreEqual("КОПАЕВ-ЗАДУНАЙСКИЙ", result["lastname"]);
            Assert.AreEqual("АНАТОЛЬЕВИЧ", result["middlename"]);
        }
    }
}